using Assets.CodeCore.Scripts.Game.Services.Commands.Impl;
using Assets.CodeCore.Scripts.Game.Services.Entitieys.Model;
using Assets.CodeCore.Scripts.Game.Services.Scripts.Data;

namespace Assets.CodeCore.Scripts.Game.Services.Commands.Profiles
{
    internal class CommonCommandProfile : ICommandProfile
    {
        public EntityTypeId EntityType => EntityTypeId.None;

        public void RegisterCommands(CommandConfigurator config)
        {
            config.Bind<Entity>("Sleep", (_, args) =>
            {
                args.ValidateCount(1);
                return new SleepCommand(args.Read<int>(0));
            });
        }
    }
}
