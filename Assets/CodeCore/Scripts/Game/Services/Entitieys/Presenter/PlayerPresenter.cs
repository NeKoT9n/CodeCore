using Assets.CodeCore.Scripts.Game.Services.Code.Model;
using Assets.CodeCore.Scripts.Game.Services.Entitieys.Model;
using Assets.CodeCore.Scripts.Game.Services.Entitieys.Presenter;
using Assets.CodeCore.Scripts.Game.Services.Scripts.View;

namespace Assets.CodeCore.Scripts.Game.Services.Entitieys.Factory.Presenters
{
    public class PlayerPresenter : EntityPresenter
    {
        public PlayerPresenter(Player player, EntityView entityView, CodeEditorService codeService)
            : base(player, entityView, codeService)
        {
        }

        public new void Initialize()
        {
            base.Initialize();

        }

        public new void Dispose()
        {
            base.Dispose();

        }
    }
}
