using Assets.CodeCore.Scripts.Game.Services.Entitieys.Model;
using System.Collections.Generic;

namespace Assets.CodeCore.Scripts.Game.Services
{
    public class EntityCommandResolver
    {
        private readonly Dictionary<string, CommandFactoryDelegate> _factories = new();

        public void Register(string commandName, CommandFactoryDelegate factory)
        {          
            _factories[commandName.ToLower()] = factory;
        }

        public bool TryCreateCommand(string commandName, Entity entity, object[] args, out ICommand command)
        {
            if (_factories.TryGetValue(commandName.ToLower(), out var factory))
            {
                command = factory(entity, args);
                return true;
            }

            command = null;
            return false;
        }

    }
}
