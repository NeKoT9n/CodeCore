using System;
using UniRx;
using UnityEngine;

namespace Assets._Shape_Escape.Scripts.Scenes.Game.Infostracture
{
    public class InputService : IInputService, IDisposable
    {
        private readonly Camera _camera = Camera.main;
        private readonly CompositeDisposable _disposables = new();

        public IObservable<Unit> OnLeftClickDown => _onLeftClickDown;
        public IObservable<Unit> OnLeftClickUp => _onLeftClickUp;
        public IReadOnlyReactiveProperty<Vector3> MouseWorldPosition => _mouseWorldPosition;

        private readonly Subject<Unit> _onLeftClickDown = new();
        private readonly Subject<Unit> _onLeftClickUp = new();
        private readonly ReactiveProperty<Vector3> _mouseWorldPosition = new();

        public void Initialize()
        {        
            Observable.EveryUpdate()
                .Subscribe(_ =>
                {
                    var pos = _camera.ScreenToWorldPoint(Input.mousePosition);
                    pos.z = 0;
                    _mouseWorldPosition.Value = pos;
                })
                .AddTo(_disposables);   
          
            Observable.EveryUpdate()
                .Where(_ => Input.GetMouseButtonDown(0))
                .Subscribe(_ => _onLeftClickDown.OnNext(Unit.Default))
                .AddTo(_disposables);

          
            Observable.EveryUpdate()
                .Where(_ => Input.GetMouseButtonUp(0))
                .Subscribe(_ => _onLeftClickUp.OnNext(Unit.Default))
                .AddTo(_disposables);
        }

        public void Dispose()
        {
            _disposables.Dispose();
        }
    }
}
