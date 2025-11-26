using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;
using static UnityEngine.Rendering.VirtualTexturing.Debugging;

namespace Assets.CodeCore.Scripts.Game.Services.SceneLoad
{
    public class AddressablesSceneLoader : ISceneLoader
    {

        private readonly Dictionary<string, AsyncOperationHandle<SceneInstance>> _loadedScenes =
        new Dictionary<string, AsyncOperationHandle<SceneInstance>>();

        public async UniTask LoadScene(string address, LoadSceneMode mode = LoadSceneMode.Single)
        {
            if (_loadedScenes.ContainsKey(address))
                return;

            bool isSingle = mode == LoadSceneMode.Single;

            var handle = Addressables.LoadSceneAsync(address, mode, activateOnLoad: true);

            await handle;

            if (handle.Status != AsyncOperationStatus.Succeeded)
                throw new System.Exception($"Failed to load scene: {address}");

            _loadedScenes[address] = handle;
        }

        public async UniTask UnloadAllScenes()
        {
            foreach(var handle in _loadedScenes.Values)
            {
                var unloadHandle = Addressables.UnloadSceneAsync(handle);
                await unloadHandle;
            }

            _loadedScenes.Clear();
        }

        public async UniTask UnloadScene(string address)
        {
            if (!_loadedScenes.TryGetValue(address, out var handle))
                return;

            var unloadHandle = Addressables.UnloadSceneAsync(handle);
            await unloadHandle;

            _loadedScenes.Remove(address);
        }
    }

    public interface ISceneLoader
    {
        public UniTask LoadScene(string address, LoadSceneMode loadMode = LoadSceneMode.Single);
        public UniTask UnloadScene(string address);
        public UniTask UnloadAllScenes();
    }

}
