using RaceMobile.Tools.Reactive;
using RaceMobile.Tools.ResourceManagment;
using RaceMobile.Base;
using UnityEngine;

namespace RaceMobile.Background
{
    internal class TapeBackgroundController : BaseController
    {
        private readonly IReadOnlySubscriptionProperty<float> leftMove;
        private readonly IReadOnlySubscriptionProperty<float> rightMove;

        private readonly SubscriptionProperty<float> diff;

        private TapeBackgroundView view;
        private ResourcePath path = new ResourcePath() { PathResource = "Prefabs/background" };

        private readonly float speed;

        public TapeBackgroundController(IReadOnlySubscriptionProperty<float> left, IReadOnlySubscriptionProperty<float> right, float speed)
        {
            this.speed = speed;
            view = LoadView();
            diff = new SubscriptionProperty<float>();

            leftMove = left;
            rightMove = right;

            leftMove.SubscribeOnChange(Move);
            rightMove.SubscribeOnChange(Move);

            view.Init(diff);

        }

        private TapeBackgroundView LoadView()
        {
            var pref = ResourceLoader.LoadPrefab(path);
            GameObject tBGv = Object.Instantiate(pref);

            AddGameObject(tBGv);
            

            return tBGv.GetComponent<TapeBackgroundView>();


        }

        protected override void OnDispose()
        {
            leftMove.UnsubscribeOnChange(Move);
            rightMove.UnsubscribeOnChange(Move);

            base.OnDispose();
        }

        private void Move(float value)
        {
            diff.Value = value * speed;
        }
        
    }
}
