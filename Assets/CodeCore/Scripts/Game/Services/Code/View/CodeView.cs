using Assets.CodeCore.Scripts.Game.UI.Base;
using System;
using TMPro;
using UnityEngine;

namespace Assets.CodeCore.Scripts.Game.Services.Code.View
{
    public class CodeView : UIBase
    {
        [SerializeField] private UIButton _closeButton;
        [SerializeField] private TMP_InputField _codeText;
        [SerializeField] private TextMeshProUGUI _nameText;

        public event Action CloseButtonClicked;

        private void OnEnable()
        {
            _closeButton.Pressed += () => CloseButtonClicked?.Invoke();
        }

        public void SetCode(string code)
        {
            _codeText.text = code;
        }

        public void SetName(string name)
        {
            _nameText.text = name;
        }

        public string GetCode()
        {
            return _codeText.text;
        }

        public void OnDisable()
        {
            _closeButton.Pressed -= () => CloseButtonClicked?.Invoke();
        }


    }
}
