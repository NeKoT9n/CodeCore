using UnityEngine;
using Zenject;

namespace Assets.CodeCore.Scripts.Game.Infostracture.Factories
{
    public abstract class ViewFactory<TView> where TView : MonoBehaviour
    {
        private readonly DiContainer _diContainer;
        private TView _prefab;

        public ViewFactory(DiContainer diContainer, TView prefab)
        {
            _diContainer = diContainer;
            _prefab = prefab;   
        }

        public TView Create()
        {
            return _diContainer.InstantiatePrefab(_prefab).GetComponent<TView>();
        }
    }
}
