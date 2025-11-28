using Assets.CodeCore.Scripts.Game.Services.Entitieys.Factory;
using Assets.CodeCore.Scripts.Game.Services.Scripts.Data;
using Assets.CodeCore.Scripts.Game.Services.Scripts.View;
using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;

namespace Assets.CodeCore.Scripts.Game.Services.Entitieys.Model
{
    public class EntityViewFactory : PluginFactoryBase<EntityTypeId, IEntityViewFactoryPlugin>
    {
        public EntityViewFactory(
            IEnumerable<IEntityViewFactoryPlugin> factories) : base(factories) { }

        public async UniTask<EntityView> Spawn(Player entity)
        {
            var factory = GetFactory(entity.Type);
            return await factory.Create(entity.Prefab, entity.SpawnPosition);
        }

    }
}
