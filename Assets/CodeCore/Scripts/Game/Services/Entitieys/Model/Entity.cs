using Assets.CodeCore.Scripts.Game.Services.Entitieys.Data;
using Assets.CodeCore.Scripts.Game.Services.Scripts.Data;
using Assets.CodeCore.Scripts.Game.Services.Scripts.Model;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Assets.CodeCore.Scripts.Game.Services.Entitieys.Model
{
    public class Entity
    {
        private readonly EntityData _entityData;

        public AssetReferenceGameObject Prefab => _entityData.Prefab;
        public string Name => _entityData.Name;
        public EntityTypeId Type => _entityData.EntityType;
        public Vector2 SpawnPosition { get; private set; }
        public Script Script { get; private set; }

        public Entity(EntityData entityData, Vector2 spawnPosition)
        {
            _entityData = entityData;
            SpawnPosition = spawnPosition;
            Script = new(Name);
        }

    }

}
