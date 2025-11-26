using Assets.CodeCore.Scripts.Game.Infostracture.Factories;
using Assets.CodeCore.Scripts.Game.Infostracture.StateMachine;
using Assets.CodeCore.Scripts.Game.Providers.Assets;
using Assets.CodeCore.Scripts.Game.Providers.Entities;
using Assets.CodeCore.Scripts.Game.Providers.Level;
using Assets.CodeCore.Scripts.Game.Services;
using Zenject;

namespace Assets.CodeCore.Scripts.Game.Infostracture.Startup.Installers
{
    public class GameInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindGameStates();
            BindFactories();
            BindProviders();

        }

        private void BindFactories()
        {
            Container.Bind<WorldFactory>().AsSingle();
            Container.Bind<GameStateFactory>().AsSingle();
        }

        private void BindProviders()
        {
            Container.BindInterfacesTo<AddressablesAssetProvider>().AsSingle();
            Container.BindInterfacesTo<LevelDataProvider>().AsSingle();
            Container.BindInterfacesTo<EntityDataProvider>().AsSingle();
        }

        private void BindGameStates()
        {
            Container.BindInterfacesAndSelfTo<GameStateMachine>().AsSingle();
        }
    }
}
