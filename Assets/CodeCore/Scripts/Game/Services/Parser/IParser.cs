using Assets.CodeCore.Scripts.Game.Services.Scripts.Model;
using System.Collections.Generic;

namespace Assets.CodeCore.Scripts.Game.Infostracture.StateMachine.States
{
    public interface IParser
    {
        public List<ParseResult> Parse(Script script);
    }

}
