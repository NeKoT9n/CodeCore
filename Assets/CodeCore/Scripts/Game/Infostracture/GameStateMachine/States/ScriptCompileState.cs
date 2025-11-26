using Assets.CodeCore.Scripts.Game.Services;
using Assets.CodeCore.Scripts.Game.Services.Entitieys.Model;
using Assets.CodeCore.Scripts.Game.Services.Scripts.Model;
using Assets.CodeCore.Scripts.Game.Startup.GameStates;
using ModestTree;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

namespace Assets.CodeCore.Scripts.Game.Infostracture.StateMachine.States
{
    public class ScriptCompileState : IEnterableState
    {
        private readonly ICompiler _scriptCompiler;
        private readonly EntityService _entityService;
        private readonly IErrorService _errorService;
        private readonly IStateSwitcher _context;

        public ScriptCompileState(
            ICompiler scriptCompiler,
            EntityService entityService,
            IErrorService errorService,
            IStateSwitcher context)
        {
            _scriptCompiler = scriptCompiler;
            _entityService = entityService;
            _errorService = errorService;
            _context = context;
        }

        public void Enter()
        {
            var entities = _entityService.Entities;

            var result = _scriptCompiler.Compile(entities);

            _errorService.Show(result.Errors);
       
        }
    }

    public interface IErrorService
    {
        public void Show(ScriptErrors errors);
    }
    public class ErrorService : IErrorService
    {
        public void Show(ScriptErrors errors)
        {
            foreach(var error in errors.GetAllErrors())
            {
                Debug.Log(error);
            }
        }
    }

    public class ScriptCompileService : ICompiler
    {
        private readonly IParser _parser;
        private readonly CommandRegistry _commandRegistry;

        public ScriptCompileService(
            IParser parser,
            CommandRegistry commandRegistry)
        {
            _parser = parser;
            _commandRegistry = commandRegistry;
        }

        public CompileResult Compile(IEnumerable<Entity> entities)
        {
            var result = new CompileResult() { Errors = new(), Commands = new() };

            foreach (var entity in entities)
            {
                (List<ICommand> commands, List<string> errors)
                    = CompileEntity(entity);

                if (commands.Count > 0)
                    result.Commands.Add(entity, commands);

                result.Errors.Add(entity.Script, errors);
            }

            return result;
        }

        public (List<ICommand> Commands, List<string> Errors) CompileEntity(Entity entity)
        {
            Script script = entity.Script;

            List<ICommand> commands = new();
            List<string> errors = new();

            foreach (var parseLine in _parser.Parse(script))
            {

                if (parseLine.IsFailure)
                {
                    errors.Add(parseLine.Error);
                    continue;
                }
                try
                {
                    var commandResult = CreateCommand(entity, parseLine);

                    if (commandResult.IsFailure)
                    {
                        errors.Add(commandResult.Error);
                        continue;
                    }
                    commands.Add(commandResult.Command);
                }
                catch (ArgumentException ex)
                {
                    errors.Add($"Argument Error (Line {parseLine.Line}): {ex.Message}");
                }

            }

            return (commands, errors);
        }

        private CreatedCommandResult CreateCommand(Entity entity, ParseResult parseResult)
        {
            return _commandRegistry.Create(
                entity.Type,
                parseResult.CommandName,
                entity, parseResult.Args);
        }
    }

    public interface ICompiler
    {
        public CompileResult Compile(IEnumerable<Entity> entities); 
    }

    public struct CompileResult
    {
        public readonly bool IsFailure => !Errors.IsEmpty();

        public ScriptErrors Errors;
        public Dictionary<Entity, List<ICommand>> Commands;
    }

    public class ScriptErrors
    {
        private readonly Dictionary<Script, List<string>> _errorScripts = new();
        public bool IsEmpty()
            => _errorScripts.Count == 0;

        public List<string> GetAllErrors()
        {
            var errorList = new List<string>();
            foreach(var errors in _errorScripts)
            {
                errorList.AddRange(errors.Value);
            }

            return errorList;
        }

        public bool TryGetErrors(Script script, out List<string> errors)
        {
            if (_errorScripts.TryGetValue(script, out List<string> errorList) == false)
            {
                errors = null;
                return false;
            }

            errors = errorList;
            return true;
        }

        public void Add(Script script, string error)
        {
            List<string> errorList = GetErrorList(script);
            errorList.Add(error);
        }

        public void Add(Script script, List<string> errors)
        {
            if (errors.IsEmpty())
                return;

            List<string> errorList = GetErrorList(script);
            errorList.AddRange(errors);
        }

        private List<string> GetErrorList(Script script)
        {
            if (_errorScripts.TryGetValue(script, out List<string> errorList) == false)
            {
                errorList = new List<string>();
                _errorScripts.Add(script, errorList);
            }

            return errorList;
        }
    }

    public interface IParser
    {
        public List<ParseResult> Parse(Script script);
    }

    public class ScriptParser : IParser
    {

        private readonly Regex _commandRegex = new Regex(
        @"^\s*(\w+)\s*\(([^)]*)\)\s*;?$",
        RegexOptions.Compiled | RegexOptions.IgnoreCase);

        public List<ParseResult> Parse(Script script)
        {
            var results = new List<ParseResult>();

            var text = script.Code;
            if (string.IsNullOrEmpty(text))
                return results;

            string[] lines = text.Split(
                new[] { '\r', '\n' },
                StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < lines.Length; i++)
            {
                string line = lines[i].Trim();
                int lineNumber = i + 1;

                if (string.IsNullOrWhiteSpace(line))
                {
                    continue;
                }
             
                results.Add(ParseLine(line, lineNumber));
            }

            return results;

        }

        private ParseResult ParseLine(string line, int lineNumber)
        {
            Match match = _commandRegex.Match(line);

            if (!match.Success)
            {       
                return new ParseResult($"Invalid command format: '{line}'", lineNumber);
            }

            string commandName = match.Groups[1].Value;
            string rawArgs = match.Groups[2].Value.Trim();

            object[] args = Array.Empty<object>();

            if (!string.IsNullOrEmpty(rawArgs))
            {
                try
                {
                    args = ParseArguments(rawArgs);
                }
                catch (FormatException ex)
                {        
                    return new ParseResult($"Argument parsing failed: {ex.Message}", lineNumber);
                }
            }

            return new ParseResult(commandName, args, lineNumber);
        }

        private object[] ParseArguments(string rawArgs)
        {
  
            string[] parts = rawArgs.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            var argsList = new List<object>();

            foreach (string part in parts)
            {
                string cleanPart = part.Trim();

                if (string.IsNullOrEmpty(cleanPart)) continue;

                if (int.TryParse(cleanPart, out int intValue))
                {
                    argsList.Add(intValue);
                }
  
                else if (float.TryParse(cleanPart, System.Globalization.NumberStyles.Any,
                                        System.Globalization.CultureInfo.InvariantCulture, out float floatValue))
                {
                    argsList.Add(floatValue);
                }
 
                else
                {
                    if (cleanPart.StartsWith("\"") && cleanPart.EndsWith("\""))
                    {
                        argsList.Add(cleanPart.Trim('"'));
                    }
                    else
                    {
                        argsList.Add(cleanPart);
                    }
                }
            }

            return argsList.ToArray();
        }
    }

    public struct ParseResult
    {
        public readonly bool IsFailure => !string.IsNullOrEmpty(Error);

        public string CommandName;
        public object[] Args;
        public string Error;
        public int Line;

        public ParseResult(string commandName, object[] args, int line)
        {
            CommandName = commandName;
            Args = args ?? Array.Empty<object>();
            Line = line;
            Error = null; 
        }

        public ParseResult(string error, int line)
        {
            CommandName = null;
            Args = Array.Empty<object>();
            Line = line;
            Error = error;
        }
    }

}
