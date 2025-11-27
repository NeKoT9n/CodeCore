using Assets.CodeCore.Scripts.Game.Services.Scripts.Model;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Assets.CodeCore.Scripts.Game.Infostracture.StateMachine.States
{
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

                var lineResult = ParseLine(line, lineNumber);
                results.Add(lineResult);
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

                if (cleanPart.StartsWith("\"") && cleanPart.EndsWith("\""))
                {
                    argsList.Add(cleanPart.Trim('"'));
                }

                else if (int.TryParse(cleanPart, out int intValue))
                {
                    argsList.Add(intValue);
                }

                else if (float.TryParse(
                    cleanPart,
                    System.Globalization.NumberStyles.Any,
                    System.Globalization.CultureInfo.InvariantCulture,
                    out float floatValue))
                {
                    argsList.Add(floatValue);
                }

                else
                {
                    throw new FormatException("Unknown argument format");
                }
         
            }

            return argsList.ToArray();
        }
    }

}
