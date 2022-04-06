using UnityEngine;
using RaceMobile.Inputs;
using JoostenProductions;
using RaceMobile.Tools.Reactive;
using UnityStandardAssets.CrossPlatformInput;

namespace RaceMobile.Inputs
{
    internal sealed class InputJoysticView : BaseInputView
    {
        public override void Init(SubscriptionProperty<float> leftMove, SubscriptionProperty<float> rightMove)
        {
            base.Init(leftMove, rightMove);
            UpdateManager.SubscribeToUpdate(Move);
        }

        private void Move()
        {
            float moveStep = 10 * Time.deltaTime * CrossPlatformInputManager.GetAxis("Horizontal");
            if (moveStep > 0)
                OnRightMove(moveStep);
            else if (moveStep < 0)
                OnLeftMove(moveStep);
        }

        private void OnDestroy()
        {
            UpdateManager.UnsubscribeFromUpdate(Move);
        }

    }
}
