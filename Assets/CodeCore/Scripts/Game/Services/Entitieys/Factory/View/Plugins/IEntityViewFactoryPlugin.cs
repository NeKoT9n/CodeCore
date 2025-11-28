using Assets.CodeCore.Scripts.Game.Services.Entitieys.Factory;
using Assets.CodeCore.Scripts.Game.Services.Scripts.Data;
using Assets.CodeCore.Scripts.Game.Services.Scripts.View;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Assets.CodeCore.Scripts.Game.Services.Entitieys.Model
{
    public interface IEntityViewFactoryPlugin : IFactoryPlugin<EntityTypeId>
    {
        public UniTask<EntityView> Create(AssetReferenceGameObject prefab, Vector2 position);
    }
}
