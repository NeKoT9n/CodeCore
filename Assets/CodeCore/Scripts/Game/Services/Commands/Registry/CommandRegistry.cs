using Assets.CodeCore.Scripts.Game.Infostracture;
using Assets.CodeCore.Scripts.Game.Services.Entitieys.Model;
using Assets.CodeCore.Scripts.Game.Services.Scripts.Data;
using ModestTree;
using System;
using System.Collections.Generic;

namespace Assets.CodeCore.Scripts.Game.Services
{
    public delegate ICommand CommandFactoryDelegate(Entity entity, object[] args);

    public class CommandRegistry
    {
        private readonly Dictionary<EntityTypeId, EntityCommandResolver> _resolvers = new();

        public EntityCommandResolver GetResolver(EntityTypeId typeId)
        {
            if (_resolvers.ContainsKey(typeId) == false)
                _resolvers[typeId] = new EntityCommandResolver();

            return _resolvers[typeId];
        }

        public CreatedCommandResult Create(EntityTypeId typeId, string commandName, Entity entity, object[] args)
        {
            CreatedCommandResult result = new();

            if (_resolvers.TryGetValue(typeId, out var resolver) == false)
                throw new Exception($"Entity type '{typeId}' not register in '{nameof(EntityCommandResolver)}'");

            if (resolver.TryCreateCommand(commandName, entity, args, out var command) == false)
                throw new ArgumentException($"Command '{commandName}' not found for entity type '{typeId}'");
   
            result.Command = command;

            return result;
        }

        public CommandConfigurator For(EntityTypeId typeId)
        {
            var resolver = GetResolver(typeId);
            return new CommandConfigurator(resolver);
        }

    }

    public struct CreatedCommandResult
    {
        public readonly bool IsFailure => !string.IsNullOrEmpty(Error);
        public ICommand Command;

        public string Error;
    }
}
