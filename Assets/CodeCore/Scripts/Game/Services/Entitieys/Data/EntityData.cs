using Assets.CodeCore.Scripts.Game.Providers;
using Assets.CodeCore.Scripts.Game.Services.Scripts.Data;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Assets.CodeCore.Scripts.Game.Services.Entitieys.Data
{
    [CreateAssetMenu(menuName = ("Data/Entities"), fileName = ("EntityData"))]
    public class EntityData : ScriptableObjectGameData
    {
        [SerializeField] private EntityTypeId _entityType;
        [SerializeField] private AssetReferenceGameObject _prefab;
        [SerializeField] private string _name;

        public EntityTypeId EntityType => _entityType;
        public AssetReferenceGameObject Prefab => _prefab;
        public string Name => _name;
    }
}
