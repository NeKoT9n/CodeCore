using System;
using UniRx;
using UnityEngine;

namespace Assets._Shape_Escape.Scripts.Scenes.Game.Infostracture
{
    public interface IInputService
    {
        public IObservable<Unit> OnLeftClickDown { get; }
        public IObservable<Unit> OnLeftClickUp { get; }
        public IReadOnlyReactiveProperty<Vector3> MouseWorldPosition {  get; }
    }
}
