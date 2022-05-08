using System.Collections.Generic;
using Tools;
using UnityEngine;


public class GameController : BaseController
{
    public GameController(
        ProfilePlayer profilePlayer,
        InputControllerType inputControllerType,
        Transform placeForUI,
        IReadOnlyList<AbilityItemConfig> configs,
        InventoryModel inventoryModel,
        GlobalEventSO eventUI,
        GlobalEventSO eventsShed,
        List<ItemConfig> itemsConfig,
        IReadOnlyList<UpgradeItemConfig> upgradeItem)
    {
        var leftMoveDiff = new SubscriptionProperty<float>();
        var rightMoveDiff = new SubscriptionProperty<float>();

        var tapeBackgroundController = new TapeBackgroundController(leftMoveDiff, rightMoveDiff);
        AddController(tapeBackgroundController);

        var inputGameController = new InputGameController(leftMoveDiff, rightMoveDiff, profilePlayer.CurrentCar, inputControllerType);
        AddController(inputGameController);

        var carController = new CarController();
        AddController(carController);

        var gameUIController = new GameUIController(placeForUI, eventUI, carController, eventsShed, itemsConfig, upgradeItem, profilePlayer);
        AddController(gameUIController);

        var abilityRepository = new AbilityRepository(configs);
        var abilitiesController = new AbilitiesController(carController, inventoryModel, abilityRepository,
            new AbilitiesCollectionViewStub());
        AddController(abilitiesController);
    }
}
