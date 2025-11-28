using Assets.CodeCore.Scripts.Game.Services.Code.Model;
using Assets.CodeCore.Scripts.Game.Services.Entitieys.Impl;
using Assets.CodeCore.Scripts.Game.Services.Entitieys.Model;
using Assets.CodeCore.Scripts.Game.Services.Entitieys.Presenter;
using UnityEngine;
using UniRx;

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

            _model.Moved.Subscribe(moveData => HandleMove(moveData.Steps, moveData.Direction));
            
        }

        private void HandleMove(int steps, Vector2 direction)
        {
            if (direction.x > 0)
                _view.MoveRight(steps);

            else
                _view.MoveLeft(steps);
        }

        public override void Dispose()
        {
            base.Dispose();

        }
    }
}
