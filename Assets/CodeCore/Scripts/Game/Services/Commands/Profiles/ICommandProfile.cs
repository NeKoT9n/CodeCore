using Assets.CodeCore.Scripts.Game.Services.Scripts.Data;

namespace Assets.CodeCore.Scripts.Game.Services
{
    public interface ICommandProfile
    {
        EntityTypeId EntityType { get; }
        void RegisterCommands(CommandConfigurator config);
    }
}
