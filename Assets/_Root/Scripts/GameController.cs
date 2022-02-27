using RaceMobile.Base;
using RaceMobile.Tools.Reactive;
using RaceMobile.Background;
using RaceMobile.Car;
using RaceMobile.Inputs;
using RaceMobile;
using RaceMobile.Reward;
using UnityEngine;


namespace RaceMobile
{
    internal class GameController : BaseController
    {
        private readonly SubscriptionProperty<float> leftMove;
        private readonly SubscriptionProperty<float> rightMove;

  

        public GameController(ProfilePlayer profilePlayer, Transform placeForUI)
        {
            leftMove = new SubscriptionProperty<float>();
            rightMove = new SubscriptionProperty<float>();


            InputController inputController = new InputController(leftMove, rightMove);
            AddController(inputController);

            CarController carController = new CarController();
            AddController(carController);

            TapeBackgroundController tapeBackgroundController = new TapeBackgroundController(leftMove, rightMove, profilePlayer.CurrentCar.Speed);
            AddController(tapeBackgroundController);

            CurrencyWindowController currencyWindow = new CurrencyWindowController(placeForUI, profilePlayer);
            AddController(currencyWindow);

            DailyRewardController dailyRewardController = new DailyRewardController(placeForUI, profilePlayer);
            AddController(dailyRewardController);
        }
    }
}