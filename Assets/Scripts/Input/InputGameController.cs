using Tools;
using UnityEngine;

public class InputGameController : BaseController
{

    public InputGameController(SubscriptionProperty<float> leftMove, SubscriptionProperty<float> rightMove, Car car, InputControllerType controller)
    {
        switch (controller)
        {
            case InputControllerType.Stick:
                BaseInputView viewStick = LoadView(new ResourcePath { PathResource = "Prefabs/Game/StickControl" });
                viewStick.Init(leftMove, rightMove, car.Speed);
                break;

            case InputControllerType.Buttons:
                BaseInputView viewButtons = LoadView(new ResourcePath { PathResource = "Prefabs/Game/ButtonControl" });
                viewButtons.Init(leftMove, rightMove, car.Speed);
                break;

            case InputControllerType.Swipe:
                new InputSwipeController(leftMove, rightMove, car.Speed);
                break;

            default:
                break;
        }
    }

    private BaseInputView LoadView(ResourcePath viewPath)
    {
        var objView = Object.Instantiate(ResourceLoader.LoadPrefab(viewPath));
        AddGameObjects(objView);

        return objView.GetComponent<BaseInputView>();
    }

}
