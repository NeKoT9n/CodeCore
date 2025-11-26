using Assets.CodeCore.Scripts.Game.Services.Scripts.View;
using Cysharp.Threading.Tasks;

namespace Assets.CodeCore.Scripts.Game.Services.Entitieys.Model
{
    public class EntityViewFactory 
    {
        private readonly WorldFactory _worldFactory;

        public EntityViewFactory(WorldFactory worldFactory)
        {
            _worldFactory = worldFactory;
        }

        public async UniTask<EntityView> Spawn(Entity entity)
        {
            var view = await _worldFactory.CreateEntity(entity.Prefab, entity.SpawnPosition);
            return view;
        }

    }
}
