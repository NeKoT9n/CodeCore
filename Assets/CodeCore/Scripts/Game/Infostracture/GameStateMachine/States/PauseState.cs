using Assets.CodeCore.Scripts.Game.Infostracture;
using Assets.CodeCore.Scripts.Game.Infostracture.StateMachine.States.Interfaces;
using System.Collections.Generic;

namespace Assets.CodeCore.Scripts.Game.Startup.GameStates.States
{
    public class PauseState : IEnterableState, IExitableState
    {
        private readonly List<IPausable> _pausables;

        public PauseState(IEnumerable<IPausable> pausables)
        {
            _pausables = new(pausables);
        }

        public void Enter()
        {
            foreach(var pausable in _pausables)
            {
                pausable.Pause();
            }
        }

        public void Exit()
        {
            foreach (var pausable in _pausables)
            {
                pausable.Unpause();
            }
        }
    }
}
