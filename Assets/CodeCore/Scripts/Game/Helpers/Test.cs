using Assets.CodeCore.Scripts.Game.Infostracture.StateMachine.States;
using Assets.CodeCore.Scripts.Game.Infostracture.StateMachine;
using UnityEngine;
using Zenject;

namespace Assets.CodeCore.Scripts.Game.Helpers
{
    public class Test : MonoBehaviour
    {
        [Inject] private IStateSwitcher _stateSwitcher;
        
        public void EnterCompileState()
        {
            _stateSwitcher.TrySwitchState<ScriptCompileState>();
        }

    }
}
