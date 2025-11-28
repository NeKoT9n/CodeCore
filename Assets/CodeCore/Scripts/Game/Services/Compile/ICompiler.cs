using Assets.CodeCore.Scripts.Game.Services.Entitieys.Model;
using System.Collections.Generic;

namespace Assets.CodeCore.Scripts.Game.Infostracture.StateMachine.States
{
    public interface ICompiler
    {
        public CompileResult Compile(IEnumerable<Player> entities); 
    }

}
