using System;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.CodeCore.Scripts.Game.UI.Base
{
    [RequireComponent(typeof(Button))]
    public class UIButton : UIBase
    {
        private Button _button;
        public event Action Pressed;

        private void Awake()
        {
            _button = GetComponent<Button>();
        }

        private void OnEnable()
        {
            _button.onClick.AddListener(() => Pressed?.Invoke());
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(() => Pressed?.Invoke());
        }

    }
}
