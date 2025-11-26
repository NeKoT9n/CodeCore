using Assets.CodeCore.Scripts.Game.Services.Entitieys.Model;
using System;

namespace Assets.CodeCore.Scripts.Game.Services
{
    public class CommandConfigurator
    {
        private readonly EntityCommandResolver _resolver;

        public CommandConfigurator(EntityCommandResolver resolver)
        {
            _resolver = resolver;
        }

        public CommandConfigurator Bind<TEntity>(string commandName, Func<TEntity, object[], ICommand> factory)
            where TEntity : Entity
        {
            CommandFactoryDelegate wrapper = (entity, args)
                => factory((TEntity)entity, args);

            _resolver.Register(commandName, wrapper);

            return this;
        }
    }
}
