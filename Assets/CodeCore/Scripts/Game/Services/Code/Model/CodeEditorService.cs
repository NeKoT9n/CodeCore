using Assets.CodeCore.Scripts.Game.Services.Scripts.Model;
using UniRx;

namespace Assets.CodeCore.Scripts.Game.Services.Code.Model
{
    public class CodeEditorService
    {
        private readonly ReactiveProperty<Script> _current = new(null);
        public IReadOnlyReactiveProperty<Script> Current => _current;

        public void OpenEditor(Script script)
        {
            _current.Value = script;
        }

        public void CloseEditor()
        {
            _current.Value = null;
        }
    }
}
