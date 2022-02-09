using RaceMobile.Base;
using RaceMobile.Tools.Reactive;
using RaceMobile.Background;


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
            TapeBackgroundController backGroundController = new TapeBackgroundController(leftMove, rightMove);
        }
    }
}