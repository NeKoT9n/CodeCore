using Assets.CodeCore.Scripts.Game.Infostracture.StateMachine;
using Assets.CodeCore.Scripts.Game.Infostracture.StateMachine.States.Interfaces;
using Assets.CodeCore.Scripts.Game.Providers.Level;
using Assets.CodeCore.Scripts.Game.Services;
using Assets.CodeCore.Scripts.Game.Services.SceneLoad;
using Cysharp.Threading.Tasks;

namespace Assets.CodeCore.Scripts.Game.Startup.GameStates.States
{
    public class LoadLevelState : IEnterableState, IExitableState
    {

        private readonly ILoadLevelService _loadLevelService;
        private readonly ILevelDataProvider _levelDataProvider;
        private readonly IStateSwitcher _stateSwitcher;
        private readonly LoadingCurtain _loadingCurtain;

        public LoadLevelState(
            ILoadLevelService loadLevelService,
            ILevelDataProvider levelDataProvider,
            IStateSwitcher stateSwitcher,
            LoadingCurtain loadingCurtain)
        {
            _loadLevelService = loadLevelService;
            _levelDataProvider = levelDataProvider;
            _stateSwitcher = stateSwitcher;
            _loadingCurtain = loadingCurtain;
        }
        public async void Enter()
        {
            LevelData levelData = _levelDataProvider.LevelData;
            await _loadLevelService.LoadLevel(levelData);

            _stateSwitcher.TrySwitchState<PlayState>(); 
        }

        public void Exit()
        {
            _loadingCurtain.Hide();
        }
    }
}
