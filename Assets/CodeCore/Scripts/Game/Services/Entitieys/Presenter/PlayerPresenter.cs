using Assets.CodeCore.Scripts.Game.Services.Code.Model;
using Assets.CodeCore.Scripts.Game.Services.Entitieys.Impl;
using Assets.CodeCore.Scripts.Game.Services.Entitieys.Model;
using Assets.CodeCore.Scripts.Game.Services.Entitieys.Presenter;
using UnityEngine;
using UniRx;
using Cysharp.Threading.Tasks;

namespace Assets.CodeCore.Scripts.Game.Services.Entitieys.Factory.Presenters
{
    public class PlayerPresenter : EntityPresenter
    {
        private readonly Player _model;
        private readonly PlayerView _view;

        public PlayerPresenter(Player player, PlayerView playerView, CodeEditorService codeService)
            : base(player, playerView, codeService)
        {
            _model = player;
            _view = playerView;
        }

        public override void Initialize()
        {
            base.Initialize();

            _model.Moved.Subscribe(moveData => HandleMove(moveData));
            
        }

        private async void HandleMove(MoveData data)
        {
            if (data.Direction.x > 0)
                await _view.MoveRight(data.Steps);

            else
                await _view.MoveLeft(data.Steps);

            data.CompletionSource.TrySetResult(AsyncUnit.Default);
        }

        

        public override void Dispose()
        {
            base.Dispose();

        }
    }
}
