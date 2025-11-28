using Assets.CodeCore.Scripts.Game.Services.Entitieys.Model;
using Assets.CodeCore.Scripts.Game.Services.Scripts.Data;
using System;
using System.Collections.Generic;

namespace Assets.CodeCore.Scripts.Game.Services
{
    public delegate ICommand CommandFactoryDelegate(Player entity, object[] args);

    public class CommandRegistry
    {
        private readonly Dictionary<EntityTypeId, EntityCommandResolver> _resolvers = new();

        public EntityCommandResolver GetResolver(EntityTypeId typeId)
        {
            if (_resolvers.ContainsKey(typeId) == false)
                _resolvers[typeId] = new EntityCommandResolver();

            return _resolvers[typeId];
        }

        public ICommand Create(EntityTypeId typeId, string commandName, Player entity, object[] args)
        {

            if (_resolvers.TryGetValue(typeId, out var resolver) == false)
                throw new Exception($"Entity type '{typeId}' not register in '{nameof(EntityCommandResolver)}'");

            if (resolver.TryCreateCommand(commandName, entity, args, out var command) == false)
                throw new CommandException($"Command '{commandName}' not found for entity type '{typeId}'");

            return command;
        }

        public CommandConfigurator For(EntityTypeId typeId)
        {
            var resolver = GetResolver(typeId);
            return new CommandConfigurator(resolver);
        }

    }

    public class CommandException : Exception
    {
        public CommandException(string message) : base(message) {}
    }

}
