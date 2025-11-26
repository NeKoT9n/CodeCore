using Assets.CodeCore.Scripts.Game.Startup.GameStates;

namespace Assets.CodeCore.Scripts.Game.Infostracture
{
    public interface IFixedUpdatable : IEnterableState
    {
        public void FixedUpdate();
    }
}
