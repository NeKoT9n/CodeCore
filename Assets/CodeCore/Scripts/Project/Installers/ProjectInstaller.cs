using Zenject;
using UnityEngine;
using Assets.CodeCore.Scripts.Game.Services.SceneLoad;

namespace Assets.CodeCore.Scripts.Project.Installers
{
   
    public class ProjectInstaller : MonoInstaller
    {
        [SerializeField] private LoadingCurtain _loadingCurtain;
        public override void InstallBindings()
        {
            Container.BindInstance(_loadingCurtain);
            Container.Bind<ISceneLoader>().To<AddressablesSceneLoader>().AsSingle();
            Container.Bind<SceneLoadService>().AsSingle().NonLazy();

            Debug.Log("Inject");
        }
    }
}
