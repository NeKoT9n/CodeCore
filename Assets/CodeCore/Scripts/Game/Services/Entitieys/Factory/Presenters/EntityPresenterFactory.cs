using Assets.CodeCore.Scripts.Game.Services.Entitieys.Model;
using Assets.CodeCore.Scripts.Game.Services.Entitieys.Presenter;
using Assets.CodeCore.Scripts.Game.Services.Scripts.Data;
using Assets.CodeCore.Scripts.Game.Services.Scripts.View;
using System;
using System.Collections.Generic;

namespace Assets.CodeCore.Scripts.Game.Services.Entitieys.Factory.Presenters
{
    public class EntityPresenterFactory : PluginFactoryBase<EntityTypeId, IEntityPresenterFactoryPlugin>
    {
        public EntityPresenterFactory(
            IEnumerable<IEntityPresenterFactoryPlugin> factories) : base(factories) { }

        public EntityPresenter Create(Player entity, EntityView view)
        {
            var factory = GetFactory(entity.Type);

            return factory.Create(entity, view);
        }
    }
}
