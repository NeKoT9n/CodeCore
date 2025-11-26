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
        private readonly CodeService _codeService;
        private readonly CodeView _codeView;

        private readonly CompositeDisposable _disposables = new();

        public CodePresenter(CodeService codeService, CodeView codeView)
        {
            _codeService = codeService;
            _codeView = codeView;
        }

        public void Initialize()
        {
            _codeService.Current
                .Pairwise()
                .Subscribe(pair => Handle(pair.Previous, pair.Current))
                .AddTo(_disposables);

            _codeView.CloseButtonClicked += Close;
        }

        private void Handle(Script previous, Script current)
        {
            if (previous != null)
                _codeService.SaveCode(previous, _codeView.GetCode());

            if(current != null)
                OpenView(current);
            else
                _codeView.Hide();
        }

        private void OpenView(Script script)
        {
            _codeView.SetName(script.Name + ".cs");
            _codeView.SetCode(script.Code);
            _codeView.Show();
        }

        private void Close()
        {   
            _codeService.Close();
        }

        public void Dispose()
        {
            _codeView.CloseButtonClicked -= Close;
            _disposables.Dispose();
        }
    }
}
