using Assets.CodeCore.Scripts.Game.Startup.GameStates;

namespace Assets.CodeCore.Scripts.Game.Infostracture.StateMachine.States.Interfaces
{
    public interface IFixedUpdatableState : IState
    {
        public void FixedUpdate();
    }

}
