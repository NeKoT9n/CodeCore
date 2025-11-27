using Assets.CodeCore.Scripts.Game.Services.Entitieys.Data;
using Assets.CodeCore.Scripts.Game.Services.Entitieys.Model;
using UnityEngine;

namespace Assets.CodeCore.Scripts.Game.Services.Entitieys.Impl
{
    public interface IMovable
    {
        public void MoveLeft(int steps);
        public void MoveRight(int steps);
    }

    public class Player : Entity, IMovable
    {
        public Player(EntityData entityData, Vector2 spawnPosition)
            : base(entityData, spawnPosition)
        {
        }

        public void MoveLeft(int steps)
        {
            Debug.Log("MoveLeft " + steps);
        }

        public void MoveRight(int steps)
        {
            Debug.Log("MoveRight " + steps);
        }
    }
}
