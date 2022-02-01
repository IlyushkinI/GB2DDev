using System;
using Tools;
using UnityEngine;
using UnityEngine.UI;

namespace Profile
{
    public class InputButtonView :BaseInputView
    {
        [SerializeField] private int _planeDistance = 3;
        [SerializeField] private Button _leftButton;
        [SerializeField] private Button _rightButton;

        public override void Init(SubscriptionProperty<float> leftMove, SubscriptionProperty<float> rightMove, float speed)
        {
            base.Init(leftMove, rightMove, speed);
            var canvas = GetComponent<Canvas>();
            canvas.worldCamera = Camera.main;
            canvas.planeDistance = _planeDistance;
            _leftButton.onClick.AddListener(OnLeftClicked);
            _rightButton.onClick.AddListener(OnRightClicked);
        }

        private void OnRightClicked()
        {
            OnRightMove(_speed);
        }

        private void OnLeftClicked()
        {
            OnLeftMove(_speed);
        }

        private void OnDestroy()
        {
            _leftButton.onClick.RemoveListener(OnLeftClicked);
            _rightButton.onClick.RemoveListener(OnRightClicked);
        }
    }
}