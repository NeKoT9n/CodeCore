using Assets.CodeCore.Scripts.Game.UI.Base;
using System;
using TMPro;
using UniRx;
using UnityEngine;

namespace Assets.CodeCore.Scripts.Game.Services.Code.View
{
    public class CodeView : UIBase
    {
        [SerializeField] private UIButton _closeButton;
        [SerializeField] private TMP_InputField _codeText;
        [SerializeField] private TextMeshProUGUI _nameText;

        private readonly Subject<string> _codeChanched = new();
        private readonly Subject<Unit> _close = new();

        public IObservable<Unit> CloseButtonClicked => _close;
        public IObservable<string> CodeChanched => _codeChanched;

        private void OnEnable()
        {
            _closeButton.Pressed += () => _close.OnNext(Unit.Default);
            _codeText.onEndEdit.AddListener(text => _codeChanched.OnNext(text));
        }

        public void SetCode(string code)
        {
            _codeText.text = code;
        }

        public void SetName(string name)
        {
            _nameText.text = name;
        }

        public void OnDisable()
        {
            _closeButton.Pressed -= () => _close.OnNext(Unit.Default);
            _codeText.onEndEdit.RemoveListener(text => _codeChanched.OnNext(text));
        }


    }
}
