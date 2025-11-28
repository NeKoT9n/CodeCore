using Assets.CodeCore.Scripts.Game.Services.Entitieys.Model;
using System.Collections.Generic;

namespace Assets.CodeCore.Scripts.Game.Services
{
    public class EntityCommandResolver
    {
        private readonly Dictionary<string, CommandFactoryDelegate> _factories = new();

        public void Register(string commandName, CommandFactoryDelegate factory)
        {          
            _factories[commandName] = factory;
        }

        public bool TryCreateCommand(string commandName, Player entity, object[] args, out ICommand command)
        {
            if (_factories.TryGetValue(commandName, out var factory))
            {
                command = factory(entity, args);
                return true;
            }

            command = null;
            return false;
        }

    }
}
