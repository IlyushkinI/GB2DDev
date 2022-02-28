using JoostenProductions;
using UnityEngine;
using RaceMobile.Tools.Reactive;


namespace RaceMobile.Inputs
{
    internal sealed class AccelerationView : BaseInputView
    {
        public override void Init(SubscriptionProperty<float> leftMove, SubscriptionProperty<float> rightMove)
        {
            base.Init(leftMove, rightMove);
            UpdateManager.SubscribeToUpdate(Move);
        }

        private void Move()
        {
            var direction = Vector3.zero;
            direction.x = -Input.acceleration.y;
            direction.z = Input.acceleration.x;


            if (direction.sqrMagnitude > 1)
                direction.Normalize();

            OnRightMove(direction.sqrMagnitude / 20);


        }

        private void OnDestroy()
        {
            UpdateManager.UnsubscribeFromUpdate(Move);
        }

    }
}
