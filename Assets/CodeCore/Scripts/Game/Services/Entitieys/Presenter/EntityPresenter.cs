using Assets.CodeCore.Scripts.Game.Services.Code.Model;
using Assets.CodeCore.Scripts.Game.Services.Entitieys.Model;
using Assets.CodeCore.Scripts.Game.Services.Scripts.View;
using System;
using UnityEngine;
using Zenject;
using UniRx;

namespace Assets.CodeCore.Scripts.Game.Services.Entitieys.Presenter
{
    public class EntityPresenter : IInitializable, IDisposable
    {
        private readonly Entity _entity;
        private readonly CodeEditorService _codeService;

        private readonly EntityView _entityView;

        private readonly CompositeDisposable _disposables = new();

        public EntityPresenter(
            Entity entity,
            EntityView entityView,
            CodeEditorService codeService)
        {
            _entity = entity;
            _codeService = codeService;
            _entityView = entityView;
        }

        public virtual void Initialize()
        {
            _entityView.ScriptViewClicked
                .Subscribe(_ => OpenScript())
                .AddTo(_disposables);
        }

        public void DestroyView()
        {
            Dispose();
            GameObject.Destroy(_entityView.gameObject);
        }

        private void OpenScript()
        {
            _codeService.OpenEditor(_entity.Script);
        }

        public virtual void Dispose()
        {
            _disposables.Dispose();
        }
    }
}
