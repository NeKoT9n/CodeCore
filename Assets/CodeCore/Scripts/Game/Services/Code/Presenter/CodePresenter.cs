using Assets.CodeCore.Scripts.Game.Services.Code.Model;
using Assets.CodeCore.Scripts.Game.Services.Code.View;
using Assets.CodeCore.Scripts.Game.Services.Scripts.Model;
using System;
using Zenject;
using UniRx;

namespace Assets.CodeCore.Scripts.Game.Services.Code.Presenter
{
    public class CodePresenter : IInitializable, IDisposable
    {
        private readonly CodeEditorService _codeService;
        private readonly CodeView _codeView;

        private readonly CompositeDisposable _disposables = new();
        private IDisposable _activeScriptSubscription;

        public CodePresenter(CodeEditorService codeService, CodeView codeView)
        {
            _codeService = codeService;
            _codeView = codeView;
        }

        public void Initialize()
        {
            _codeService.Current
                .Subscribe(HandleActiveScriptChanged)
                .AddTo(_disposables);

            _codeView.CloseButtonClicked
                .Subscribe(_ => _codeService.CloseEditor());
        }

        private void HandleActiveScriptChanged(Script script)
        {
            _activeScriptSubscription?.Dispose();

            if (script == null)
            {
                _codeView.Hide();
                return;
            }

            _codeView.SetCode(script.Code);
            _codeView.SetName(script.Name);
            _codeView.Show();

            _activeScriptSubscription = _codeView.CodeChanched
                .Subscribe(code => script.SetCode(code));
        }

        public void Dispose()
        {
           
            _disposables.Dispose();
        }
    }
}
