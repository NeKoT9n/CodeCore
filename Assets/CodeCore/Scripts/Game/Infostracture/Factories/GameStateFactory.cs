using Assets.CodeCore.Scripts.Game.Startup.GameStates;
using Zenject;

namespace Assets.CodeCore.Scripts.Game.Infostracture.Factories
{
    public class GameStateFactory 
    {
        private readonly DiContainer _diContainer;

        public GameStateFactory(DiContainer diContainer)
        {
            _diContainer = diContainer;
        }

        public TState CreateState<TState>() where TState : class, IState
        {
            return _diContainer.Instantiate<TState>();
        }
    }
}