using Assets.CodeCore.Scripts.Game.Services.Scripts.Data;
using System;
using UniRx;
using UnityEngine;

namespace Assets.CodeCore.Scripts.Game.Services.Scripts.View
{
    public class EntityView : WorldView
    {
        [SerializeField] private EntityTypeId _entityTypeId;
        [SerializeField] private ScriptView _scriptView;

        private readonly Subject<Unit> _scriptClicked = new();
        public IObservable<Unit> ScriptViewClicked => _scriptClicked;

        private void OnEnable()
        {
            _scriptView.Clicked += () => _scriptClicked.OnNext(Unit.Default);
        }

        private void OnDisable()
        {
            _scriptView.Clicked -= () => _scriptClicked.OnNext(Unit.Default);
        }

    }
}
