using Tools;

public class GameController : BaseController
{
    public GameController(ProfilePlayer profilePlayer, InputControllerType inputControllerType)
    {
        var leftMoveDiff = new SubscriptionProperty<float>();
        var rightMoveDiff = new SubscriptionProperty<float>();
        
        var tapeBackgroundController = new TapeBackgroundController(leftMoveDiff, rightMoveDiff);
        AddController(tapeBackgroundController);

        var inputGameController = new InputGameController(leftMoveDiff, rightMoveDiff, profilePlayer.CurrentCar, inputControllerType);
        AddController(inputGameController);
            
        var carController = new CarController();
        AddController(carController);
    }
}

