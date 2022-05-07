using System.Collections.Generic;
using UnityEngine;


public class ShedController : BaseController, IShedController
{
    private readonly Car _car;
    private readonly UpgradeHandlerRepository _upgradeRepository;
    private readonly InventoryController _inventoryController;
        
    public ShedController(IReadOnlyList<UpgradeItemConfig> upgradeItems, List<ItemConfig> items, Car car, Transform placeForUI, GlobalEventSO eventsShed)
    {
        _car = car;

        _upgradeRepository = new UpgradeHandlerRepository(upgradeItems);
        AddController(_upgradeRepository);
        
        _inventoryController = new InventoryController(items, upgradeItems, placeForUI, eventsShed);
        AddController(_inventoryController);
    }

    public void Enter()
    {
        _inventoryController.ShowInventory(_car.Speed, _car.Control);
        Debug.Log($"Enter, car speed = {_car.Speed} + {_car.Control}");
    }

    public void Exit()
    {
        UpgradeCarWithEquipedItems(_car, _inventoryController.EquipedItems, _upgradeRepository.UpgradeItems);
        Debug.Log($"Exit, car speed = {_car.Speed} + {_car.Control}");
    }

    private void UpgradeCarWithEquipedItems(
        IUpgradeableCar car,
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