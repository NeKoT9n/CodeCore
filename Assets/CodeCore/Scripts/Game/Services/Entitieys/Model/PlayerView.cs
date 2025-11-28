using Assets.CodeCore.Scripts.Game.Services.Scripts.View;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;


namespace Assets.CodeCore.Scripts.Game.Services.Entitieys.Model
{
    public class PlayerView : EntityView
    {
        [SerializeField] private float _stepDuration;

        private Tween _moveTween;
        public async UniTask MoveLeft(int steps)
        {
            await Move(-steps);
        }

        public async UniTask MoveRight(int steps)
        {
            await Move(steps);
        }

        private async UniTask Move(int steps)
        {
            _moveTween?.Kill();

            _moveTween = transform
                .DOMoveX(steps, _stepDuration * Mathf.Abs(steps))
                .SetRelative()
                .SetEase(Ease.InElastic);

            await _moveTween.ToUniTask(
                TweenCancelBehaviour.KillAndCancelAwait,
                this.GetCancellationTokenOnDestroy());
        }
    }
}
