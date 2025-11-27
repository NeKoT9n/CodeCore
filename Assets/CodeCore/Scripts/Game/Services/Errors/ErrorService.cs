using UnityEngine;

namespace Assets.CodeCore.Scripts.Game.Infostracture.StateMachine.States
{
    public class ErrorService : IErrorService
    {
        public void Show(ScriptErrors errors)
        {
            foreach(var error in errors.GetAllErrors())
            {
                Debug.Log(error);
            }
        }
    }

}
