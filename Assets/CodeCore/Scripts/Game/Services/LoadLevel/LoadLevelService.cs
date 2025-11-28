using Assets.CodeCore.Scripts.Game.Services.Entitieys.Model;
using Cysharp.Threading.Tasks;

namespace Assets.CodeCore.Scripts.Game.Services
{
    public class LoadLevelService : ILoadLevelService
    {
        private readonly EntityFactory _entityFactory;
        private readonly EntityService _entityService;

        public LoadLevelService(EntityFactory entityFactory, EntityService entityService)
        {
            _entityFactory = entityFactory;
            _entityService = entityService;
        }

        public UniTask LoadLevel(LevelData levelData)
        {

            var entities = _entityFactory.Create(levelData.SpawnPoints);
            _entityService.Add(entities);

            return UniTask.CompletedTask;
            
        }
    }
}
