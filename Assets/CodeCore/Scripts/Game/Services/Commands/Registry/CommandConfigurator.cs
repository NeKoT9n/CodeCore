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

    public readonly struct ArgsReader
    {
        private readonly string _commandName;
        private readonly object[] _args;

        public ArgsReader(string commandName, object[] args)
        {
            _commandName = commandName;
            _args = args;
        }

        public int Count => _args.Length;

        public T Read<T>(int index)
        {
            if (index >= _args.Length)
            {
                throw new ArgumentException(
                    $"Command '{_commandName}': Missing argument at position {index + 1}. Expected at least {index + 1}, got {_args.Length}.");
            }

            object arg = _args[index];

            try
            {
                return (T)Convert.ChangeType(arg, typeof(T));
            }
            catch
            {
                throw new ArgumentException(
                    $"Command '{_commandName}': Argument {index + 1} ('{arg}') must be of type '{typeof(T).Name}'.");
            }
        }

        public void ValidateCount(int expected)
        {
            if (_args.Length != expected)
                throw new ArgumentException($"Command '{_commandName}' expects exactly {expected} argument(s), but got {_args.Length}.");
        }
    }
}
