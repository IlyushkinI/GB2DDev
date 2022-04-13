using JoostenProductions;
using Tools;
using UnityEngine;

namespace CarInput
{
    public class SwipeInputView : BaseInputView
    {
        private const float _swipeAcceleration = 0.1f;
        private const float _slowUpPerSecond = 0.5f;
        private float _currentTouchX;

        public override void Init(SubscriptionProperty<float> leftMove, SubscriptionProperty<float> rightMove, float speed)
        {
            base.Init(leftMove, rightMove, speed);
            UpdateManager.SubscribeToUpdate(OnUpdate);
            _speed = 0;
        }

        private void OnDestroy()
        {
            UpdateManager.UnsubscribeFromUpdate(OnUpdate);
        }

        private void OnUpdate()
        {
          
            if (Input.touchCount > 0)
            {
                var touch = Input.GetTouch(0);
                if (touch.phase == TouchPhase.Began)
                {
                    _currentTouchX = touch.position.x;
                  
                }

                if (touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary)
                {

                    var step = 0f;

                    if (touch.position.x != _currentTouchX)
                    {
                        step = touch.position.x - _currentTouchX;
                        _currentTouchX = touch.position.x;
                    }

                    AddAcceleration(step * Time.deltaTime * _swipeAcceleration);
                }
            }
            Move();
            Slowdown();
        }
        private void AddAcceleration(float acc)
        {
            _speed = Mathf.Clamp(_speed + acc, -1f, 1f);
        }

        private void Move()
        {
            if (_speed > 0)
                OnRightMove(_speed);
            else if (_speed < 0)
                OnLeftMove(_speed);
        }

        private void Slowdown()
        {
            var sgn = Mathf.Sign(_speed);
            _speed = Mathf.Clamp01(Mathf.Abs(_speed) - _slowUpPerSecond * Time.deltaTime) * sgn;
        }
    }
}