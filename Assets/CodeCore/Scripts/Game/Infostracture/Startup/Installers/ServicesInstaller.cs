using Assets.CodeCore.Scripts.Commands;
using Assets.CodeCore.Scripts.Game.Services;
using Assets.CodeCore.Scripts.Game.Services.Code.Model;
using Assets.CodeCore.Scripts.Game.Services.Commands.Registry;
using Assets.CodeCore.Scripts.Game.Services.Entitieys.Model;
using System;
using Zenject;

namespace Assets.CodeCore.Scripts.Game.Startup
{
    public class ServicesInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container
                .Bind<ILoadLevelService>()
                .To<LoadLevelService>()
                .AsSingle();

            Container
                .Bind<CodeService>()
                .AsSingle();

            Container
                .Bind<EntityService>()
                .AsSingle();

            BindFactories();
            BindCommands();
        }

        private void BindFactories()
        {
            Container.Bind<EntityFactory>().AsSingle();
        }

        private void BindCommands()
        {
            Container.Bind<ICommandService>().To<CommandService>().AsSingle();
            Container.Bind<CommandRegistryService>().AsSingle();
            Container.Bind<CommandRegistry>().AsSingle();

            BindProfiles();
        }

        private void BindProfiles()
        {
            Container.Bind<ICommandProfile>().To<PlayerCommandProfile>().AsTransient();
            //..
        }
    }
}
