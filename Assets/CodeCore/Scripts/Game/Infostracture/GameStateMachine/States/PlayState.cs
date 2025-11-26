using Assets.CodeCore.Scripts.Game.Infostracture;
using Assets.CodeCore.Scripts.Game.Infostracture.StateMachine.States.Interfaces;
using System.Collections.Generic;

namespace Assets.CodeCore.Scripts.Game.Startup.GameStates.States
{
    public class PlayState : IUpdatableState
    { 
        private readonly List<IUpdatable> _updatables;

        public PlayState(IEnumerable<IUpdatable> updatables)
        {
            _updatables = new(updatables);
        }

        public void Enter() { }

        public void Update()
        {
            foreach(IUpdatable updatable in _updatables)
            {
                updatable.Update();
            }
        }
    }
}
