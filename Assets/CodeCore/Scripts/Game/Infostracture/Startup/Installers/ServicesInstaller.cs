using Assets.CodeCore.Scripts.Commands;
using Assets.CodeCore.Scripts.Game.Infostracture.StateMachine.States;
using Assets.CodeCore.Scripts.Game.Services;
using Assets.CodeCore.Scripts.Game.Services.Code.Model;
using Assets.CodeCore.Scripts.Game.Services.Commands.Registry;
using Assets.CodeCore.Scripts.Game.Services.Entitieys.Model;
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

            BindCompileSystem();
            BindFactories();
            BindCommands();
        }

        private void BindCompileSystem()
        {
            Container
                .Bind<ICompiler>()
                .To<ScriptCompileService>()
                .AsSingle();

            Container
               .Bind<IErrorService>()
               .To<ErrorService>()
               .AsSingle();

            Container
               .Bind<IParser>()
               .To<ScriptParser>()
               .AsSingle();
        }

        private void BindFactories()
        {
            Container.Bind<EntityFactory>().AsSingle();
        }

        private void BindCommands()
        {
            Container.BindInterfacesAndSelfTo<CommandRegistryService>().AsSingle();
            Container.Bind<ICommandService>().To<CommandService>().AsSingle();
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
