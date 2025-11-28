using Assets.CodeCore.Scripts.Game.Services.Scripts.Data;
using Assets.CodeCore.Scripts.Game.Services.Scripts.View;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Assets.CodeCore.Scripts.Game.Services.Entitieys.Model
{
    public class PlayerViewFactoryPlugin : IEntityViewFactoryPlugin
    {
        private readonly WorldFactory _worldFactory;
        public EntityTypeId Key => EntityTypeId.Player;

        public PlayerViewFactoryPlugin(WorldFactory worldFactory)
        {
            _worldFactory = worldFactory;
        }

        public async UniTask<EntityView> Create(AssetReferenceGameObject prefab, Vector2 position)
        {
            return await _worldFactory.CreateEntity<EntityView>(prefab, position);
        }
    }
}
