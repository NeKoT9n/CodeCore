using Assets.CodeCore.Scripts.Game.Services.Entitieys.Data;
using Assets.CodeCore.Scripts.Game.Services.Entitieys.Model;
using System;
using UniRx;
using UnityEngine;

namespace Assets.CodeCore.Scripts.Game.Services.Entitieys.Impl
{
    public interface IMovable
    {
        public void Move(int steps, Vector2 direction);
    }

    public class Player : Entity, IMovable
    {

        private readonly Subject<MoveData> _move = new();
        public IObservable<MoveData> Moved => _move; 
        public Player(EntityData entityData, Vector2 spawnPosition)
            : base(entityData, spawnPosition)
        {
        }

        public void Move(int steps, Vector2 direction)
        {
            _move.OnNext(new(steps,direction));
        }

    }

    public struct MoveData
    {
        public int Steps;
        public Vector2 Direction;

        public MoveData(int steps, Vector2 direction)
        {
            Steps = steps;
            Direction = direction;
        }
    }
}
