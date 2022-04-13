using Tools;
using CarInput;
using Profile;

public class GameController : BaseController
{
    public GameController(ProfilePlayer profilePlayer)
    {
        var leftMoveDiff = new SubscriptionProperty<float>();
        var rightMoveDiff = new SubscriptionProperty<float>();
        
        var tapeBackgroundController = new TapeBackgroundController(leftMoveDiff, rightMoveDiff);
        AddController(tapeBackgroundController);

        var gameInputType = InputType.SWIPE;
        var inputGameController = new InputGameController(leftMoveDiff, rightMoveDiff, profilePlayer.CurrentCar, gameInputType);
        AddController(inputGameController);
            
        var carController = new CarController();
        AddController(carController);
    }
}

