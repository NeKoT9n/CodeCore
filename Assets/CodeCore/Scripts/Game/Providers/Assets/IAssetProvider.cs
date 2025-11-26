using Assets.CodeCore.Scripts.Game.Services;
using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Assets.CodeCore.Scripts.Game.Providers.Assets
{
    public interface IAssetProvider
    {
        public UniTask<TResult> Load<TResult>(AssetReference assetReference) where TResult : class;
        public UniTask<TResult> LoadGameObject<TResult>(AssetReference assetReference) where TResult : Object;
        public UniTask<TResult> Load<TResult>(string assetReferenceName) where TResult : class;
        public void ReleaseAll();

    }

    public class AddressablesAssetProvider : IAssetProvider
    {
        private readonly Dictionary<string, AsyncOperationHandle> _completedCached = new();
        private readonly Dictionary<string, AsyncOperationHandle<GameObject>> _completedCachedGameObjects = new();
        private readonly Dictionary<string, List<AsyncOperationHandle>> _handles = new();

        public async UniTask<TResult> Load<TResult>(AssetReference assetReference) where TResult : class
        {
            if (_completedCached.TryGetValue(assetReference.AssetGUID, out AsyncOperationHandle completedAssetReference))
            {
                return completedAssetReference.Result as TResult;
            }

            AsyncOperationHandle<TResult> handle = Addressables.LoadAssetAsync<TResult>(assetReference);


            handle.Completed += h =>
            {
                _completedCached[assetReference.AssetGUID] = h;
            };

            AddHandle(assetReference.AssetGUID, handle);

            return await handle.Task;
        }

        public async UniTask<TResult> LoadGameObject<TResult>(AssetReference assetReference) where TResult : Object
        {
            if (_completedCachedGameObjects.TryGetValue(assetReference.AssetGUID, out AsyncOperationHandle<GameObject> completedAssetReference))
            {
                return completedAssetReference.Result.GetComponent<TResult>();
            }

            AsyncOperationHandle<GameObject> handle = Addressables.LoadAssetAsync<GameObject>(assetReference);

            handle.Completed += h =>
            {
                _completedCachedGameObjects[assetReference.AssetGUID] = h;
            };

            AddHandle(assetReference.AssetGUID, handle);

            return (await handle.Task).GetComponent<TResult>();
        }

        public async UniTask<TResult> Load<TResult>(string assetReferenceName) where TResult : class
        {
            if (_completedCached.TryGetValue(assetReferenceName, out AsyncOperationHandle completedAssetReference))
            {
                return completedAssetReference.Result as TResult;
            }

            AsyncOperationHandle<TResult> handle = Addressables.LoadAssetAsync<TResult>(assetReferenceName);

            handle.Completed += h =>
            {
                _completedCached[assetReferenceName] = h;
            };

            AddHandle(assetReferenceName, handle);

            return await handle.Task;
        }

        private void AddHandle<TResult>(string key, AsyncOperationHandle<TResult> handle) where TResult : class
        {
            if (!_handles.TryGetValue(key, out List<AsyncOperationHandle> resourceHandles))
            {
                resourceHandles = new();
                _handles[key] = resourceHandles;
            }

            resourceHandles.Add(handle);
        }

        public void ReleaseAll()
        {
            foreach (List<AsyncOperationHandle> resourceHandles in _handles.Values)
            {
                foreach (AsyncOperationHandle handle in resourceHandles)
                {
                    Addressables.Release(handle);
                }
            }
        }

    }
}
