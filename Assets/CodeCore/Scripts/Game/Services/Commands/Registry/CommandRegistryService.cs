using Assets.CodeCore.Scripts.Game.Infostracture;
using System.Collections.Generic;

namespace Assets.CodeCore.Scripts.Game.Services.Commands.Registry
{
    public class CommandRegistryService : IInitializable
    {

        private readonly CommandRegistry _registry;
        private readonly List<ICommandProfile> _profiles;

        public CommandRegistryService(
            CommandRegistry commandRegistry,
            List<ICommandProfile> profiles)
        {
            _registry = commandRegistry;
            _profiles = profiles;
        }

        public void Initialize()
        {
            foreach(var profile in _profiles)
            {
                profile.RegisterCommands(_registry.For(profile.EntityType));
            }
        }
    }
}
