using Assets.CodeCore.Scripts.Game.Services.Scripts.Model;
using UniRx;

namespace Assets.CodeCore.Scripts.Game.Services.Code.Model
{
    public class CodeService
    {
        private ReactiveProperty<Script> _current = new(null);
        public IReadOnlyReactiveProperty<Script> Current => _current;

        public void OpenScript(Script script)
        {
            _current.Value = script;
        }

        public void SaveCode(Script script, string codeText)
        {
            script?.SetCode(codeText); 
        }

        public void Close()
        {
            _current.Value = null;
        }
    }
}
