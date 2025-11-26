using Cysharp.Threading.Tasks;

namespace Assets.CodeCore.Scripts.Game.Services
{
    public interface ILoadLevelService
    {
        public UniTask LoadLevel(LevelData levelData);
    }
}
