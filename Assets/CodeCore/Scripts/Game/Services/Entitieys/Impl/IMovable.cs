using Assets.CodeCore.Scripts.Game.Services.Entitieys.Data;
using Assets.CodeCore.Scripts.Game.Services.Entitieys.Model;
using System;
using UnityEngine;

namespace Assets.CodeCore.Scripts.Game.Services.Entitieys.Impl
{
    public interface IMovable
    {
        public void Move(int steps, Vector2 direction);
    }

    public class Player : Model.Player, IMovable
    {

        public event Action<int, Vector2> MoveAction;
        public Player(EntityData entityData, Vector2 spawnPosition)
            : base(entityData, spawnPosition)
        {
        }

        public void Move(int steps, Vector2 direction)
        {
            MoveAction?.Invoke(steps, direction);
        }

    }
}
