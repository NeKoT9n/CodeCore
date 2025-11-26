using Assets.CodeCore.Scripts.Game.Infostracture;
using Assets.CodeCore.Scripts.Game.Providers.Assets;
using Assets.CodeCore.Scripts.Game.Services;
using Cysharp.Threading.Tasks;

namespace Assets.CodeCore.Scripts.Game.Providers.Level
{
    public interface ILevelDataProvider
    {
        public LevelData  LevelData { get; }
    }

    public class LevelDataProvider : IAsyncInitializable, ILevelDataProvider
    {
        private LevelData _levelData;
        private readonly IAssetProvider _assetProvider;

        public LevelData LevelData => _levelData;

        public LevelDataProvider(IAssetProvider assetProvider)
        {
            _assetProvider = assetProvider;
        }

        public async UniTask Initialize()
        {
            _levelData = await _assetProvider.Load<LevelData>("1_LevelData");
        }
    }
}
