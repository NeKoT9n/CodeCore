using Assets.CodeCore.Scripts.Game.Services.Scripts.Model;
using ModestTree;
using System.Collections.Generic;

namespace Assets.CodeCore.Scripts.Game.Infostracture.StateMachine.States
{
    public class ScriptErrors
    {
        private readonly Dictionary<Script, List<string>> _scriptErrors = new();
        public bool IsEmpty()
            => _scriptErrors.Count == 0;

        public List<string> GetAllErrors()
        {
            var errorList = new List<string>();
            foreach(var errors in _scriptErrors)
            {
                errorList.AddRange(errors.Value);
            }

            return errorList;
        }

        public bool TryGetErrors(Script script, out List<string> errors)
        {
            if (_scriptErrors.TryGetValue(script, out List<string> errorList) == false)
            {
                errors = null;
                return false;
            }

            errors = errorList;
            return true;
        }

        public void Add(Script script, string error)
        {
            List<string> errorList = GetErrorList(script);
            errorList.Add(error);
        }

        public void Add(Script script, List<string> errors)
        {
            if (errors.IsEmpty())
                return;

            List<string> errorList = GetErrorList(script);
            errorList.AddRange(errors);
        }

        private List<string> GetErrorList(Script script)
        {
            if (_scriptErrors.TryGetValue(script, out List<string> errorList) == false)
            {
                errorList = new List<string>();
                _scriptErrors.Add(script, errorList);
            }

            return errorList;
        }
    }

}
