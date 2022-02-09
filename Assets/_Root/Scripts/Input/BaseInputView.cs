using UnityEngine;
using RaceMobile.Tools.Reactive;



namespace RaceMobile.Input
{
    internal abstract class BaseInputView : MonoBehaviour
    {
        private SubscriptionProperty<float> leftMove;
        private SubscriptionProperty<float> rightMove;

        public virtual void Init(SubscriptionProperty<float> leftMove, SubscriptionProperty<float> rightMove)
        {
            this.leftMove = leftMove;
            this.rightMove = rightMove;
        }

        protected void OnLeftMove(float value)
        {
            leftMove.Value = value;
        }

        protected void OnRightMove(float value)
        {
            rightMove.Value = value;
        }
    }
}