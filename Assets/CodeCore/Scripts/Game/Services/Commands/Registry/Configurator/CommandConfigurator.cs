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
                TEntity concreteEntity = entity as TEntity ?? throw new InvalidOperationException(
                        $"Type mismatch during command creation '{commandName}'. " +
                        $"Expected entity type '{typeof(TEntity).Name}', but received '{entity.GetType().Name}'. " +
                        "Check CommandRegistry logic."
                    );

                var reader = new ArgsReader(commandName, rawArgs);

                return factory(concreteEntity, reader);
            });

            return this;
        }
    }
}
