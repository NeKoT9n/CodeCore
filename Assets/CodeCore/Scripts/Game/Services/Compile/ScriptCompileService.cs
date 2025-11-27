using Assets.CodeCore.Scripts.Game.Services;
using Assets.CodeCore.Scripts.Game.Services.Entitieys.Model;
using Assets.CodeCore.Scripts.Game.Services.Scripts.Model;
using System;
using System.Collections.Generic;

namespace Assets.CodeCore.Scripts.Game.Infostracture.StateMachine.States
{
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

                if (commands != null && commands.Count > 0)
                    result.Commands.Add(entity, commands);

                if(errors != null && errors.Count > 0)
                    result.Errors.Add(entity.Script, errors);
            }

            return result;
        }

        public (List<ICommand> Commands, List<string> Errors) CompileEntity(Entity entity)
        {
            Script script = entity.Script;
            if (script == null)
                return (null, null);

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
                    var command = CreateCommand(entity, parseLine);
                    commands.Add(command);
                }
                catch (ArgumentException ex)
                {
                    errors.Add($"Argument Error (Line {parseLine.Line}): {ex.Message}");
                }
                catch (CommandException ex)
                {
                    errors.Add($"Command Error (Line {parseLine.Line}): {ex.Message} {ex.Message}");
                }

            }

            return (commands, errors);
        }

        private ICommand CreateCommand(Entity entity, ParseResult parseResult)
        {
            return _commandRegistry.Create(
                entity.Type,
                parseResult.CommandName,
                entity, parseResult.Args);
        }
    }

}
