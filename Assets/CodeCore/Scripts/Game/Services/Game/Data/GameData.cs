using Assets.CodeCore.Scripts.Game.Providers;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Assets.CodeCore.Scripts.Game.Services.Game.Data
{
    [CreateAssetMenu(menuName = ("Data/GameData"), fileName = ("GameData"))]
    public class GameData : ScriptableObjectGameData
    {
        [SerializeField] private List<AssetReferenceT<LevelData>> _levelsData;
        public IReadOnlyList<AssetReferenceT<LevelData>> LevelData => _levelsData;

    }
}
