using Assets.CodeCore.Scripts.Game.Startup.GameStates;

namespace Assets.CodeCore.Scripts.Game.Infostracture.StateMachine
{
    public interface IStateSwitcher
    {
        public bool TrySwitchState<TState>() where TState : IState;
    }
}
