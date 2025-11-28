using Assets.CodeCore.Scripts.Game.Services.Scripts.View;
using DG.Tweening;
using System;
using UnityEngine;

namespace Assets.CodeCore.Scripts.Game.Services.Entitieys.Model
{
    public class PlayerView : EntityView
    {
        [SerializeField] private float _stepDuration;

        private Tween _moveTween;
        public void MoveLeft(int steps, Action callback = null)
        {
            Move(-steps, callback);
        }

        public void MoveRight(int steps, Action callback = null)
        {
            Move(steps, callback);
        }

        private void Move(int steps, Action callback)
        {
            _moveTween?.Kill();

            _moveTween = transform
                .DOMoveX(steps, _stepDuration * Mathf.Abs(steps))
                .SetRelative()
                .OnComplete(() => callback?.Invoke());
        }
    }
}
