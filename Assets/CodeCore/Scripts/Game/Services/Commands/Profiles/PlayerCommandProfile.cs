using Assets.CodeCore.Scripts.Game.Services.Entitieys.Impl;
using Assets.CodeCore.Scripts.Game.Services.Scripts.Data;
using System.Numerics;

namespace Assets.CodeCore.Scripts.Game.Services
{
    public class PlayerCommandProfile : ICommandProfile
    {
        public EntityTypeId EntityType => EntityTypeId.Player;

        public void RegisterCommands(CommandConfigurator config)
        {

            config.Bind<Player>("MoveRight", (player, args) =>
            {
                args.ValidateCount(1);
                return new MoveCommand(player, args.Read<int>(0), new Vector2(1, 0));
            });

            config.Bind<Player>("MoveLeft", (player, args) =>
            {
                args.ValidateCount(1);
                return new MoveCommand(player, args.Read<int>(0), new Vector2(-1, 0));
            });

        }
    }
}

