using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Assets.CodeCore.Scripts.Game.Services.Entitieys.Impl
{
    public interface IMovable
    {
        public UniTask Move(int steps, Vector2 direction);
    }

    public struct MoveData
    {
        public int Steps;
        public Vector2 Direction;
        public UniTaskCompletionSource<AsyncUnit> CompletionSource;

        public MoveData(
            int steps,
            Vector2 direction,
            UniTaskCompletionSource<AsyncUnit> completion)
        {
            Steps = steps;
            Direction = direction;
            CompletionSource = completion;
        }
    }
}
