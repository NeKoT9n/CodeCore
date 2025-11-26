using Assets.CodeCore.Scripts.Game.UI.Base;
using System;
using UnityEngine;

namespace Assets.CodeCore.Scripts.Game.Services.Scripts.View
{
    [RequireComponent(typeof(UIButton))]
    public class ScriptView : UIBase
    {
        private UIButton _button;
        public event Action Edit;

        private void Awake()
        {
            _button = GetComponent<UIButton>();
        }

        private void OnEnable()
        {
            _button.Pressed += () => Edit?.Invoke();
        }

        private void OnDisable()
        {
            _button.Pressed -= () => Edit?.Invoke();
        }
    }
}
