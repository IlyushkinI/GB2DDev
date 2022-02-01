using JoostenProductions;
using Tools;
using UnityEngine;

namespace Profile
{
    public class InputSwipeView : BaseInputView
    {
        [SerializeField] private SwipeHolder _swipeHolder;
        [SerializeField] private int _planeDistance = 3;
        [SerializeField] private float _duration = 1;

        private bool _isOnMoving;
        private float _screenWight;
        private float _lerpProgress;
        private float _currentPosition;
        private float _targetPosition;

        public override void Init(SubscriptionProperty<float> leftMove, SubscriptionProperty<float> rightMove,
            float speed)
        {
            base.Init(leftMove, rightMove, speed);
            var canvas = GetComponent<Canvas>();
            canvas.worldCamera = Camera.main;
            canvas.planeDistance = _planeDistance;
            _screenWight = Screen.width;
            _swipeHolder = GetComponentInChildren<SwipeHolder>();
            //UpdateManager.SubscribeToUpdate(Transition);
        }

        private void OnEnable()
        {
            _swipeHolder.Swipe += Move;
        }

        private void OnDisable()
        {
            _swipeHolder.Swipe -= Move;
        }

        // private void Transition()
        // {
        //     if (_isOnMoving)
        //     {
        //         _lerpProgress += Time.deltaTime / _duration;
        //         var temp = Mathf.Lerp(_currentPosition, _targetPosition, _lerpProgress);
        //         
        //         Move(temp);
        //         if (_lerpProgress >= 1)
        //         {
        //             _isOnMoving = false;
        //             _lerpProgress = 0;
        //             _targetPosition = 0;
        //         }
        //     }
        // }

        // private void SetDistance(float distance)
        // {
        //     if (distance == 0) return;
        //
        //     var partOfScreen = distance / _screenWight;
        //     _isOnMoving = true;
        //     _currentPosition = _leftMoveValue;
        //     if (_targetPosition == 0)
        //     {
        //         _targetPosition = _currentPosition + partOfScreen ;
        //     }
        //     else
        //     {
        //         _targetPosition += partOfScreen;
        //     }
        // }

        private void Move(float distance)
        {
            if (distance > 0)
            {
                OnRightMove(_speed);
            }
            else
            {
                OnLeftMove(_speed);
            }
        }

        protected void OnDestroy()
        {
            //UpdateManager.UnsubscribeFromUpdate(Transition);
        }
    }
}