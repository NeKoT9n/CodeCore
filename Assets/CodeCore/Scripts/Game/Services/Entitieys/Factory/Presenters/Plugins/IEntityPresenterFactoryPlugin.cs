using Assets.CodeCore.Scripts.Game.Services.Entitieys.Model;
using Assets.CodeCore.Scripts.Game.Services.Entitieys.Presenter;
using Assets.CodeCore.Scripts.Game.Services.Scripts.Data;
using Assets.CodeCore.Scripts.Game.Services.Scripts.View;

namespace Assets.CodeCore.Scripts.Game.Services.Entitieys.Factory.Presenters
{
    public interface IEntityPresenterFactoryPlugin : IFactoryPlugin<EntityTypeId>
    {
        public EntityPresenter Create(Player player, EntityView view);
    }
}
