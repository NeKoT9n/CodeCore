using Assets.CodeCore.Scripts.Game.Services.Entitieys.Model;
using Assets.CodeCore.Scripts.Game.Startup.GameStates;
using Assets.CodeCore.Scripts.Game.Startup.GameStates.States;

namespace Assets.CodeCore.Scripts.Game.Infostracture.StateMachine.States
{
    public class ScriptCompileState : IEnterableState
    {
        private readonly ICompiler _scriptCompiler;
        private readonly EntityService _entityService;
        private readonly IErrorService _errorService;
        private readonly IStateSwitcher _context;

        public ScriptCompileState(
            ICompiler scriptCompiler,
            EntityService entityService,
            IErrorService errorService,
            IStateSwitcher context)
        {
            _scriptCompiler = scriptCompiler;
            _entityService = entityService;
            _errorService = errorService;
            _context = context;
        }

        public void Enter()
        {
            var entities = _entityService.Entities;

            var result = _scriptCompiler.Compile(entities);

            _errorService.Show(result.Errors);
            _context.TrySwitchState<CodingState>();
       
        }
    }

}
