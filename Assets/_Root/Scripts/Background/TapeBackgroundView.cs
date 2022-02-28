using UnityEngine;
using RaceMobile.Tools.Reactive;

namespace RaceMobile.Background
{
    internal class TapeBackgroundView : MonoBehaviour
    {
        [SerializeField]
        private Background[] backgrounds;

        private IReadOnlySubscriptionProperty<float> diff;

        public void Init(IReadOnlySubscriptionProperty<float> diff)
        {
            this.diff = diff;
            this.diff.SubscribeOnChange(Move);
        }

        private void OnDestroy()
        {
            diff?.UnsubscribeOnChange(Move);
        }


        private void Move(float value)
        {
            foreach (var background in backgrounds)
            {
                background.Move(-value);
            }
        }
    }
}
