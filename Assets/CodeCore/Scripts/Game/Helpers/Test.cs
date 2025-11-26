using Assets.CodeCore.Scripts.Game.Services;
using Assets.CodeCore.Scripts.Game.Services.SceneLoad;
using UnityEditor.Build.Content;
using UnityEngine;
using Zenject;

namespace Assets.CodeCore.Scripts.Game.Helpers
{
    public class Test : MonoBehaviour
    {
        [Inject] private SceneLoadService _loadLevelService;
        
        public async void LoadMainMenu()
        {
            await _loadLevelService.LoadMainMenuScene();
        }

    }
}
