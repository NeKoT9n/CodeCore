using Assets.CodeCore.Scripts.Game.Helpers;
using Assets.CodeCore.Scripts.Game.Providers;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.CodeCore.Scripts.Game.Services
{
    [CreateAssetMenu(menuName =("Data/LevelData"), fileName =("0_LevelData"))]
    public class LevelData : ScriptableObjectGameData
    {
        [SerializeField] private string _name;
        [SerializeField] private List<SpawnPoint> _spawnPoints;

        public List<SpawnPoint> SpawnPoints => _spawnPoints;
        public string Name => _name;

        public void SetSpawnPoints(List<SpawnPoint> points)
        {
            _spawnPoints = points;
        }
    }
}
