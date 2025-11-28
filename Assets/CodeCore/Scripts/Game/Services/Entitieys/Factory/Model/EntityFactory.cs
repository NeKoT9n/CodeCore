using Assets.CodeCore.Scripts.Game.Helpers;
using Assets.CodeCore.Scripts.Game.Providers.Entities;
using Assets.CodeCore.Scripts.Game.Services.Entitieys.Data;
using Assets.CodeCore.Scripts.Game.Services.Entitieys.Factory;
using Assets.CodeCore.Scripts.Game.Services.Entitieys.Factory.Model;
using Assets.CodeCore.Scripts.Game.Services.Entitieys.Model;
using Assets.CodeCore.Scripts.Game.Services.Scripts.Data;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.CodeCore.Scripts.Game.Services
{
    public class EntityFactory : PluginFactoryBase<EntityTypeId, IEntityFactoryPlugin>
    {
        private readonly IEntityDataProvider _entityDataProvider;
 
        public EntityFactory(
            IEntityDataProvider entityDataProvider,
            IEnumerable<IEntityFactoryPlugin> factories) : base (factories)
        {
            _entityDataProvider = entityDataProvider;      
        }

        public Entity Create(EntityTypeId typeId, Vector2 position)
        {
            EntityData data = _entityDataProvider.GetBy(typeId);
            var factory = GetFactory(typeId);

            return factory.Create(data, position);
        }

        public List<Entity> Create(List<SpawnPoint> spawnPoints)
        {
            var spawnedEntity = new List<Entity>(spawnPoints.Count);

            foreach (var spawnPoint in spawnPoints)
            {
                var entity = Create(spawnPoint.EntityType, spawnPoint.Position);
                spawnedEntity.Add(entity);
            }

            return spawnedEntity;
        }
    }
}
