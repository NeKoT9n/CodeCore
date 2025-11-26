using Assets.CodeCore.Scripts.Game.Services.Code.Model;
using Assets.CodeCore.Scripts.Game.Services.Entitieys.Model;
using Assets.CodeCore.Scripts.Game.Services.Scripts.View;
using System;
using UnityEngine;
using Zenject;

namespace Assets.CodeCore.Scripts.Game.Services.Entitieys.Presenter
{
    public class EntityPresenter : IInitializable, IDisposable
    {
        private readonly Entity _entity;
        private readonly CodeService _codeService;

        private readonly EntityView _entityView;
        private readonly ScriptView _scriptView;

        public EntityPresenter(
            Entity entity,
            EntityView entityView,
            CodeService codeService)
        {
            _entity = entity;
            _codeService = codeService;
            _entityView = entityView;
            _scriptView = _entityView.ScriptView;
        }
        public void Initialize()
        {
            _scriptView.Edit += OpenScript;
        }

        public void DestroyView()
        {
            Dispose();
            GameObject.Destroy(_entityView.gameObject);
        }

        private void OpenScript()
        {
            _codeService.OpenScript(_entity.Script);
        }

        public void Dispose()
        {
            _scriptView.Edit -= OpenScript;
        }
    }
}
