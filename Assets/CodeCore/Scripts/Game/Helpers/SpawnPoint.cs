using Assets.CodeCore.Scripts.Game.Services.Scripts.Data;
using System;
using UnityEngine;

namespace Assets.CodeCore.Scripts.Game.Helpers
{
    [Serializable]
    public class SpawnPoint
    {
        [SerializeField] private EntityTypeId _entityType;
        [SerializeField] private Vector2 _position;

        public EntityTypeId EntityType => _entityType;
        public Vector2 Position => _position;
        public SpawnPoint(Vector2 position, EntityTypeId entityType)
        {
            _position = position;
            _entityType = entityType;
        }
    }
}
