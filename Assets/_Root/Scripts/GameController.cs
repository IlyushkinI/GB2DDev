﻿using RaceMobile.Base;
using RaceMobile.Tools.Reactive;
using RaceMobile.Background;
using RaceMobile.Car;
using RaceMobile.Input;


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

            InputController inputController = new InputController(leftMove, rightMove);
            AddController(inputController);

            CarController carController = new CarController();
            AddController(carController);

            TapeBackgroundController tapeBackgroundController = new TapeBackgroundController(leftMove, rightMove);
            AddController(tapeBackgroundController);
        }
    }
}