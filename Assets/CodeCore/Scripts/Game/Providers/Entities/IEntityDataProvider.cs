using Assets.CodeCore.Scripts.Game.Infostracture;
using Assets.CodeCore.Scripts.Game.Providers.Assets;
using Assets.CodeCore.Scripts.Game.Services.Entitieys.Data;
using Assets.CodeCore.Scripts.Game.Services.Scripts.Data;
using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Assets.CodeCore.Scripts.Game.Providers.Entities
{
    public interface IEntityDataProvider
    {
        public EntityData GetBy(EntityTypeId entityType);
    }

    public class EntityDataProvider : IEntityDataProvider, IAsyncInitializable
    {
        private Dictionary<EntityTypeId, EntityData> _entities;
        private readonly IAssetProvider _assetProvider;

        public EntityDataProvider(IAssetProvider assetProvider)
        {
            _assetProvider = assetProvider;
        }

        public async UniTask Initialize()
        {
            _entities = new();

            var playerData = await _assetProvider.Load<EntityData>("PlayerData");
            var enemyData = await _assetProvider.Load<EntityData>("EnemyData");

            _entities.Add(playerData.EntityType, playerData);
            _entities.Add(enemyData.EntityType, enemyData);
        }

        public EntityData GetBy(EntityTypeId entityType)
        {
            if (_entities.TryGetValue(entityType, out var entityData) == false)
                throw new ArgumentException("No data:" + entityType);

            return entityData;
        }
    }
}
