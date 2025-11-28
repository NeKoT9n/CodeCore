using Assets.CodeCore.Scripts.Game.Providers.Assets;
using Assets.CodeCore.Scripts.Game.Providers.Entities;
using Assets.CodeCore.Scripts.Game.Services.Entitieys.Data;
using Assets.CodeCore.Scripts.Game.Services.Scripts.Data;
using Assets.CodeCore.Scripts.Game.Services.Scripts.View;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Zenject;

namespace Assets.CodeCore.Scripts.Game.Services
{
    public class WorldFactory
    {
        private readonly IAssetProvider _assetProvider;
        private readonly IEntityDataProvider _entityDataProvider;
        private readonly DiContainer _diContainer;

        public WorldFactory(
            IAssetProvider assetProvider,
            IEntityDataProvider entityDataProvider,
            DiContainer diContainer)
        {
            _assetProvider = assetProvider;
            _entityDataProvider = entityDataProvider;
            _diContainer = diContainer;
        }

        public async UniTask<Tview> CreateEntity<Tview>(AssetReferenceGameObject reference, Vector2 position)
            where Tview: EntityView
        {
            var prefab = await _assetProvider.LoadGameObject<EntityView>(reference);
            var go = _diContainer
                .InstantiatePrefab(prefab, position, Quaternion.identity, null)
                .GetComponent<Tview>();

            return go; 
        }


    }
}