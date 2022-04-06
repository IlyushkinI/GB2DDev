using UnityEngine;
using JoostenProductions;
using RaceMobile.Tools.Reactive;

namespace RaceMobile.Inputs
{
    internal class TouchInputView : BaseInputView
    {

        private float tapAcceleration = 0.1f;
        private float speedDown = 0.4f;
        private float speed = 0.0f;
        public override void Init(SubscriptionProperty<float> leftMove, SubscriptionProperty<float> rightMove)
        {
            base.Init(leftMove, rightMove);
            UpdateManager.SubscribeToUpdate(OnUpdate);
        }

        private void OnUpdate()
        {
            if(Input.touchCount > 0)
            {
                var touch = Input.GetTouch(0);
                var halfScreenWidth = Screen.width / 2;

                if(touch.phase == TouchPhase.Began)
                {
                    if(touch.position.x > halfScreenWidth)
                    {
                        AddAcceleration(tapAcceleration);
                    }
                    else if(touch.position.x <= halfScreenWidth)
                    {
                        AddAcceleration(-tapAcceleration);
                    }
                }
            }

            Move();
        }

        private void SlowDown()
        {

        }


        private void AddAcceleration(float acc)
        {
            speed = Mathf.Clamp(speed + acc, -1f, 1f);
        }

        private void Move()
        {
            if (speed < 0)
                OnLeftMove(speed);
            else
                OnRightMove(speed);
        }

        private void OnDestroy()
        {
            UpdateManager.UnsubscribeFromUpdate(OnUpdate);
        }
    }
}
