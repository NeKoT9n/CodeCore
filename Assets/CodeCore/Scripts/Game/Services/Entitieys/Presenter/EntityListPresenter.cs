using Assets.CodeCore.Scripts.Game.Services.Entitieys.Model;
using Zenject;
using UniRx;
using Assets.CodeCore.Scripts.Game.Services.Code.Model;
using System.Collections.Generic;
using System;
using Assets.CodeCore.Scripts.Game.Services.Entitieys.Factory.Presenters;

namespace Assets.CodeCore.Scripts.Game.Services.Entitieys.Presenter
{
    public class EntityListPresenter : IInitializable, IDisposable
    {
        private readonly EntityService _entityService;
        private readonly CodeEditorService _codeService;
        private readonly EntityViewFactory _entityViewFactory;
        private readonly EntityPresenterFactory _entityPresenterFactory;

        private readonly Dictionary<Player, EntityPresenter> _entities = new();

        private readonly CompositeDisposable _disposables = new();

        public EntityListPresenter(
            EntityService entityService,
            CodeEditorService codeService,
            EntityViewFactory entityViewFactory,
            EntityPresenterFactory entityPresenterFactory)
        {
            _entityService = entityService;
            _codeService = codeService;
            _entityViewFactory = entityViewFactory;
            _entityPresenterFactory = entityPresenterFactory;
        }

        public void Initialize()
        {
            _entityService.Entities
                .ObserveAdd()
                .Subscribe(CreateEntity)
                .AddTo(_disposables);

            _entityService.Entities
                .ObserveRemove()
                .Subscribe(RemoveEntity)
                .AddTo(_disposables);
        }

        private async void CreateEntity(CollectionAddEvent<Player> e)
        {
            var view = await _entityViewFactory.Spawn(e.Value);
            EntityPresenter presenter = _entityPresenterFactory.Create(e.Value, view);
            presenter.Initialize();
            _entities.Add(e.Value, presenter);
        }

        private void RemoveEntity(CollectionRemoveEvent<Player> e)
        {
            var presenter = _entities[e.Value];
            presenter.DestroyView();
            _entities.Remove(e.Value);
        }

        public void Dispose()
        {
            foreach(var entity in _entities)
            {
                entity.Value.Dispose();
            }

        }
    }
}
