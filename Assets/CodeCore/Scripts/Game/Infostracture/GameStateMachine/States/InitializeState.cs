using Assets.CodeCore.Scripts.Game.Infostracture;
using Assets.CodeCore.Scripts.Game.Infostracture.StateMachine;
using System.Collections.Generic;



namespace Assets.CodeCore.Scripts.Game.Startup.GameStates.States
{
    public class InitializeState : IEnterableState
    {
        private readonly List<IInitializable> _initializables;
        private readonly List<IAsyncInitializable> _asyncInitializables;

        private readonly IStateSwitcher _context;

        public InitializeState(
            List<IInitializable> initializable,
            List<IAsyncInitializable> asyncInitializable,
            IStateSwitcher context)
        {
            _initializables = initializable;
            _asyncInitializables = asyncInitializable;
            _context = context;
        }

        public async void Enter()
        {
            foreach (var asyncInitializable in _asyncInitializables)
                await asyncInitializable.Initialize();

            foreach(var initializable in _initializables)
                initializable.Initialize();

            _context.TrySwitchState<LoadLevelState>();
        }

    }
}
