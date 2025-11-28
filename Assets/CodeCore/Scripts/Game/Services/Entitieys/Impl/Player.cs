using Assets.CodeCore.Scripts.Game.Services.Entitieys.Data;
using Assets.CodeCore.Scripts.Game.Services.Entitieys.Model;
using Cysharp.Threading.Tasks;
using System;
using UniRx;
using UnityEngine;

namespace Assets.CodeCore.Scripts.Game.Services.Entitieys.Impl
{
    public class Player : Entity, IMovable
    {

        private readonly Subject<MoveData> _move = new();
        public IObservable<MoveData> Moved => _move; 
        public Player(EntityData entityData, Vector2 spawnPosition)
            : base(entityData, spawnPosition)
        {
        }

        public UniTask Move(int steps, Vector2 direction)
        {
            var completionSource = new UniTaskCompletionSource<AsyncUnit>();
            var moveData = new MoveData(steps, direction, completionSource);

            _move.OnNext(moveData);

            return completionSource.Task;
        }

    }
}
