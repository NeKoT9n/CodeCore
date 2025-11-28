using Assets.CodeCore.Scripts.Game.Services.Entitieys.Data;
using Assets.CodeCore.Scripts.Game.Services.Entitieys.Impl;
using Assets.CodeCore.Scripts.Game.Services.Entitieys.Model;
using Assets.CodeCore.Scripts.Game.Services.Scripts.Data;
using UnityEngine;

namespace Assets.CodeCore.Scripts.Game.Services.Entitieys.Factory.Model
{
    public class PlayerFactoryPlugin : IEntityFactoryPlugin
    {
        public EntityTypeId Key => EntityTypeId.Player;

        public Entitieys.Model.Entity Create(EntityData entityData, Vector2 spawnPosition)
        {
            return new Impl.Player(entityData, spawnPosition);
        }
    }
}
