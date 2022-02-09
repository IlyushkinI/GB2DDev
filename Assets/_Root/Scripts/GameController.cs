using RaceMobile.Base;
using RaceMobile.Tools.Reactive;

namespace RaceMobile
{
    internal class GameController : BaseController
    {
        private readonly SubscriptionProperty<float> leftMove;
        private readonly SubscriptionProperty<float> rightMove;

        public GameController()
        {
            //InputController
            //Car
            BackgroundController backGroundController = new BackgroundController(leftMove, rightMove);
        }
    }

    internal sealed class BackgroundController : BaseController
    {
        public BackgroundController(SubscriptionProperty<float> leftMove, SubscriptionProperty<float> rightMove)
        {
            

        }

    }
}