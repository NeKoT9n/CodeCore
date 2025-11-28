using Assets.CodeCore.Scripts.Game.Infostracture;
using Assets.CodeCore.Scripts.Game.Infostracture.StateMachine.States.Interfaces;
using Assets.CodeCore.Scripts.Game.Services.Code.Model;
using System.Collections.Generic;

namespace Assets.CodeCore.Scripts.Game.Startup.GameStates.States
{
    public class CodingState : IUpdatableState, IExitableState
    { 
        private readonly List<IUpdatable> _updatables;
        private readonly CodeEditorService _codeService;

        public CodingState(IEnumerable<IUpdatable> updatables, CodeEditorService codeService)
        {
            _updatables = new(updatables);
            _codeService = codeService;
        }

        public void Update()
        {
            foreach(IUpdatable updatable in _updatables)
            {
                updatable.Update();
            }
        }
        public void Exit()
        {
            _codeService.CloseEditor();
        }
    }
}
