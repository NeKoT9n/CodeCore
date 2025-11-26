using Assets.CodeCore.Scripts.Game.Services.Scripts.View;
using Zenject;

namespace Assets.CodeCore.Scripts.Game.Services.Scripts.Factory
{
    public class ScriptViewFactory
    {
        private readonly DiContainer _diContainer;
        private ScriptView _prefab;

        public ScriptViewFactory(DiContainer diContainer)
        {
            _diContainer = diContainer;
        }

        public ScriptView Create()
        {
            var go = _diContainer.InstantiatePrefab(_prefab);

            return go.GetComponent<ScriptView>();
        }
    }
}
