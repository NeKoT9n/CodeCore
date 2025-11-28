using Assets.CodeCore.Scripts.Game.Services.Entitieys.Model;
using System.Collections.Generic;

namespace Assets.CodeCore.Scripts.Game.Infostracture.StateMachine.States
{
    public struct CompileResult
    {
        public readonly bool IsFailure => !Errors.IsEmpty();

        public ScriptErrors Errors;
        public Dictionary<Entity, List<ICommand>> Commands;
    }

}
