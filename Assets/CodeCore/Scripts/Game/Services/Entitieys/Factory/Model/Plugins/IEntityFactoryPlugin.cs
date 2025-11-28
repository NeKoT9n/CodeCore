using Assets.CodeCore.Scripts.Game.Services.Entitieys.Data;
using Assets.CodeCore.Scripts.Game.Services.Entitieys.Model;
using Assets.CodeCore.Scripts.Game.Services.Scripts.Data;
using UnityEngine;

namespace Assets.CodeCore.Scripts.Game.Services.Entitieys.Factory.Model
{
    public interface IEntityFactoryPlugin : IFactoryPlugin<EntityTypeId>
    {
        public Entity Create(EntityData entityData, Vector2 spawnPosition);
    }
}
