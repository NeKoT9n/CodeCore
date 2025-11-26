using Assets.CodeCore.Scripts.Game.Services.Entitieys.Model;
using Assets.CodeCore.Scripts.Game.Services.Scripts.Data;
using System;
using System.Numerics;

namespace Assets.CodeCore.Scripts.Game.Services
{
    public class PlayerCommandProfile : ICommandProfile
    {
        public EntityTypeId EntityType => EntityTypeId.Player;

        public void RegisterCommands(CommandConfigurator config)
        {
            config
                .Bind<Entity>("MoveLeft", (player, args) =>
                    new MoveCommand(player, Convert.ToInt32(args[0]), new Vector2(-1,0)));

            config
                .Bind<Entity>("MoveRight", (player, args) =>
                    new MoveCommand(player, Convert.ToInt32(args[0]), new Vector2(1, 0)));
        }
    }
}
