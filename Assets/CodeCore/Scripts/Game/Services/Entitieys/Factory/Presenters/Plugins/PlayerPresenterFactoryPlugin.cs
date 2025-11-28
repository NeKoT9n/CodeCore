using Assets.CodeCore.Scripts.Game.Services.Code.Model;
using Assets.CodeCore.Scripts.Game.Services.Entitieys.Model;
using Assets.CodeCore.Scripts.Game.Services.Entitieys.Presenter;
using Assets.CodeCore.Scripts.Game.Services.Scripts.Data;
using Assets.CodeCore.Scripts.Game.Services.Scripts.View;

namespace Assets.CodeCore.Scripts.Game.Services.Entitieys.Factory.Presenters
{
    public class PlayerPresenterFactoryPlugin : IEntityPresenterFactoryPlugin
    {
        private readonly CodeEditorService _codeEditorService;
        public EntityTypeId Key => EntityTypeId.Player;

        public PlayerPresenterFactoryPlugin(CodeEditorService codeEditorService)
        {
            _codeEditorService = codeEditorService;
        }

        public EntityPresenter Create(Player player, EntityView view)
        {
            return new PlayerPresenter(player, view, _codeEditorService);
        }
    }
}
