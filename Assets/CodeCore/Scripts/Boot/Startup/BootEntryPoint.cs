using Assets.CodeCore.Scripts.Game.Services.SceneLoad;
using UnityEngine;
using Zenject;

namespace Assets.CodeCore.Scripts.Boot.Startup
{
    public class BootEntryPoint : MonoBehaviour
    {
        [Inject] private readonly SceneLoadService _sceneLoadService;

        private async void Awake()
        {
            await _sceneLoadService.LoadGameScene("Level_1"); 
        }
    }
}
