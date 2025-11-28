using Assets.CodeCore.Scripts.Commands;
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
        private readonly ICommandService _commandService;
        private readonly IStateSwitcher _context;

        public ScriptCompileState(
            ICompiler scriptCompiler,
            EntityService entityService,
            IErrorService errorService,
            ICommandService commandService,
            IStateSwitcher context)
        {
            _scriptCompiler = scriptCompiler;
            _entityService = entityService;
            _errorService = errorService;
            _commandService = commandService;
            _context = context;
        }

        public void Enter()
        {
            var entities = _entityService.Entities;

            var result = _scriptCompiler.Compile(entities);

            foreach(var valuePair in result.Commands)
            {
                var commands = valuePair.Value;
                _commandService.Execute(commands, 5f);
            } // test only

            _errorService.Show(result.Errors);
            _context.TrySwitchState<CodingState>();
       
        }
    }

}
