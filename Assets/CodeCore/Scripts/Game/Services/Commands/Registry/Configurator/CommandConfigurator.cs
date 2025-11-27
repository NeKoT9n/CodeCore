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

        public CommandConfigurator Bind<TEntity>(
            string commandName,
            Func<TEntity, ArgsReader, ICommand> factory)
            where TEntity : Entity
        {
            _resolver.Register(commandName, (entity, rawArgs) =>
            {
                var reader = new ArgsReader(commandName, rawArgs);

                return factory((TEntity)entity, reader);
            });

            return this;
        }
    }
}
