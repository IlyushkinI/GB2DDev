using System.Collections.Generic;
using UnityEngine;

public class ShedController : BaseController, IShedController
{
    private readonly IReadOnlyList<UpgradeItemConfig> _upgradeItems;
    private readonly Car _car;
    private readonly UpgradeHandlerRepository _upgradeRepository;
    private readonly InventoryController _inventoryController;
    private readonly InventoryModel _model;
    private readonly ResourcePath _viewPath = new ResourcePath { PathResource = "Prefabs/Shed" };
    private readonly ShedView _view;
    private ProfilePlayer _profilePlayer;

    public ShedController(
        Transform placeForUi, 
        IReadOnlyList<UpgradeItemConfig> upgradeItems, 
        List<ItemConfig> items, 
        ProfilePlayer profilePlayer)
    {
        _upgradeItems = upgradeItems;
        _profilePlayer = profilePlayer;
        _car = profilePlayer.CurrentCar;
        _upgradeRepository = new UpgradeHandlerRepository(upgradeItems);
        _view = LoadView(placeForUi);
        _view.ExitShed(Exit);
        _model = new InventoryModel();
        AddController(_upgradeRepository);
        _inventoryController = new InventoryController(items, _model);
        AddController(_inventoryController);
    }

    private ShedView LoadView(Transform placeForUi)
    {
        var objectView = Object.Instantiate(ResourceLoader.LoadPrefab(_viewPath), placeForUi, false);
        AddGameObjects(objectView);

        return objectView.GetComponent<ShedView>();
    }

    public void Enter()
    {
        _inventoryController.ShowInventory();
        _view.DropdownItems(_inventoryController.GetItems());
        Debug.Log($"Enter, car speed = {_car.Speed}");
    }

    public void Exit()
    {
        UpgradeCarWithEquipedItems(_car, _model.GetEquippedItems(), _upgradeRepository.Content);
        Debug.Log($"Exit, car speed = {_car.Speed}");

        _profilePlayer.CurrentState.Value = Profile.GameState.Start;
    }

    private void UpgradeCarWithEquipedItems(IUpgradeableCar car,
        IReadOnlyList<IItem> equiped,
        IReadOnlyDictionary<int, IUpgradeCarHandler> upgradeHandlers)
    {
        foreach (var item in equiped)
        {
            if (upgradeHandlers.TryGetValue(item.Id, out var handler))
                handler.Upgrade(car);
        }
    }
}