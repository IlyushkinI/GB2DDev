using System.Collections.Generic;
using UnityEngine;

public class ShedController : BaseController, IShedController
{
    private readonly IReadOnlyList<UpgradeItemConfig> _upgradeItems;
    private readonly Car _car;
    private readonly UpgradeHandlerRepository _upgradeRepository;
    private readonly InventoryController _inventoryController;
    private readonly InventoryModel _model;

    public ShedController(IReadOnlyList<UpgradeItemConfig> upgradeItems, List<ItemConfig> items, Car car, Transform placeForUI, MainMenuView view, ProfilePlayer profilePlayer)
    {
        _upgradeItems = upgradeItems;
        _car = car;
        _model = new InventoryModel();
        _upgradeRepository = new UpgradeHandlerRepository();
        _inventoryController = new InventoryController(items, _model, placeForUI, _upgradeItems, _upgradeRepository, profilePlayer);
        _inventoryController.InitInventory(view);
        
        AddController(_upgradeRepository);
        AddController(_inventoryController);
    }



    public void Enter()
    {
        _inventoryController.ShowInventory();
        Debug.Log($"1. Enter, car speed = {_car.Speed}");
    }

    public void Exit()
    {
        UpgradeCarWithEquipedItems(_car, _model.GetEquippedItems(), _upgradeRepository.UpgradeItems);
        Debug.Log($"2. Exit, car speed = {_car.Speed}");
    }

    private void UpgradeCarWithEquipedItems(IUpgradeableCar car,
        IReadOnlyList<IItem> equiped,
        IReadOnlyDictionary<int, IUpgradeCarHandler> upgradeHandlers)
    {
            foreach (var item in equiped)
            {
                if (upgradeHandlers.TryGetValue(item.Id, out var handler))
                {
                    handler.Upgrade(car);
                }

            }
      
    }

    protected override void OnDispose()
    {
        base.OnDispose();
        _inventoryController.Dispose();
    }
}