using Assets.CodeCore.Scripts.Game.Helpers;
using Assets.CodeCore.Scripts.Game.Providers.Entities;
using Assets.CodeCore.Scripts.Game.Services.Entitieys.Data;
using Assets.CodeCore.Scripts.Game.Services.Entitieys.Impl;
using Assets.CodeCore.Scripts.Game.Services.Entitieys.Model;
using Assets.CodeCore.Scripts.Game.Services.Scripts.Data;
using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.CodeCore.Scripts.Game.Services
{
    public class LoadLevelService : ILoadLevelService
    {
        private readonly EntityFactory _entityFactory;
        private readonly EntityService _entityService;

        public LoadLevelService(EntityFactory entityFactory, EntityService entityService)
        {
            _entityFactory = entityFactory;
            _entityService = entityService;
        }

        public async UniTask LoadLevel(LevelData levelData)
        {

            var entities = _entityFactory.Create(levelData.SpawnPoints);
            _entityService.Add(entities);
            
        }
    }

    public class EntityFactory
    {
        private readonly IEntityDataProvider _entityDataProvider;

        public EntityFactory(IEntityDataProvider entityDataProvider)
        {
            _entityDataProvider = entityDataProvider;
        }

        public Entity Create(EntityTypeId typeId, Vector2 position)
        {
            EntityData data = _entityDataProvider.GetBy(typeId);

            return typeId switch
            {
                EntityTypeId.Player => new Player(data, position),
                _ => new(data, position),
            };
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
