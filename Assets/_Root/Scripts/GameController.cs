using RaceMobile.Base;
using RaceMobile.Tools.Reactive;
using RaceMobile.Background;
using RaceMobile.Car;


namespace RaceMobile
{
    internal class GameController : BaseController
    {
        private readonly SubscriptionProperty<float> leftMove;
        private readonly SubscriptionProperty<float> rightMove;

        public GameController()
        {
            leftMove = new SubscriptionProperty<float>();
            rightMove = new SubscriptionProperty<float>();
            //InputController
            CarController carController = new CarController();
            AddController(carController);

            TapeBackgroundController tapeBackgroundController = new TapeBackgroundController(leftMove, rightMove);
            AddController(tapeBackgroundController);
        }
    }
}