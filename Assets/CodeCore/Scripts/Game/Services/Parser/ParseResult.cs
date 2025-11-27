using System;

namespace Assets.CodeCore.Scripts.Game.Infostracture.StateMachine.States
{
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
