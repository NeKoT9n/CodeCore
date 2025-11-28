using Assets.CodeCore.Scripts.Game.Services.Code.Presenter;
using Assets.CodeCore.Scripts.Game.Services.Code.View;
using Assets.CodeCore.Scripts.Game.Services.Entitieys.Factory.Presenters;
using Assets.CodeCore.Scripts.Game.Services.Entitieys.Model;
using Assets.CodeCore.Scripts.Game.Services.Entitieys.Presenter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Assets.CodeCore.Scripts.Game.Infostracture.Startup.Installers
{
    public class ViewInstaller : MonoInstaller
    {
        [SerializeField] private CodeView _codeView;
        public override void InstallBindings()
        {
            BindViewFactories();
            BindPresenterFactories();

            Container
                .BindInterfacesTo<EntityListPresenter>()
                .AsSingle()
                .NonLazy();

            Container
               .BindInterfacesTo<CodePresenter>()
               .AsSingle()
               .WithArguments(_codeView)
               .NonLazy();
        }

        private void BindPresenterFactories()
        {
            Container.Bind<EntityPresenterFactory>().AsSingle();

            Container.Bind<IEntityPresenterFactoryPlugin>().To<PlayerPresenterFactoryPlugin>().AsTransient();
        }

        private void BindViewFactories()
        {
            Container.Bind<EntityViewFactory>().AsSingle();

            Container.Bind<IEntityViewFactoryPlugin>().To<PlayerViewFactoryPlugin>().AsTransient();
        }
    }
}
