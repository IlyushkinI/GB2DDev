using System.Collections.Generic;
using UnityEngine;


public class ShedController : BaseController, IShedController
{
    private readonly Car _car;
    private readonly InventoryController _inventoryController;
    private List<UpgradeItemConfig> _upgradeItemsEnter;

    public ShedController(IReadOnlyList<UpgradeItemConfig> upgradeItems, List<ItemConfig> items, Car car, Transform placeForUI, GlobalEventSO eventsShed)
    {
        _car = car;
        
        _inventoryController = new InventoryController(items, upgradeItems, placeForUI, eventsShed);
        AddController(_inventoryController);
    }

    public void Enter()
    {
        Time.timeScale = 0.0f;
        _upgradeItemsEnter = new List<UpgradeItemConfig>(_car.AppliedItems);
        _inventoryController.ShowInventory(_car);
    }

    public void Exit()
    {
        var addItemsRepo = new UpgradeHandlerRepository(NeedAdd(_upgradeItemsEnter, _car.AppliedItems));
        AddController(addItemsRepo);

        var removeItemsRepo = new UpgradeHandlerRepository(NeedRemove(_upgradeItemsEnter, _car.AppliedItems));
        AddController(removeItemsRepo);

        UpgradeCarWithEquipedItems(_car, _inventoryController.EquipedItems, addItemsRepo.UpgradeItems, true);
        UpgradeCarWithEquipedItems(_car, _inventoryController.EquipedItems, removeItemsRepo.UpgradeItems, false);
        Time.timeScale = 1.0f;
    }

    private List<UpgradeItemConfig> NeedAdd (List<UpgradeItemConfig> listA, List<UpgradeItemConfig> listB)
    {
        List<UpgradeItemConfig> listC = new List<UpgradeItemConfig>();

        foreach (var itemB in listB)
        {
            if (!listA.Contains(itemB))
            {
                listC.Add(itemB);
            }
        }

        return listC;
    }

    private List<UpgradeItemConfig> NeedRemove(List<UpgradeItemConfig> listA, List<UpgradeItemConfig> listB)
    {
        List<UpgradeItemConfig> listC = new List<UpgradeItemConfig>();

        foreach (var itemA in listA)
        {
            if (!listB.Contains(itemA))
            {
                listC.Add(itemA);
            }
        }

        return listC;
    }

    private void UpgradeCarWithEquipedItems(
        IUpgradeableCar car,
        IReadOnlyList<IItem> equiped,
        IReadOnlyDictionary<int, IUpgradeCarHandler> upgradeHandlers,
        bool isAdd)
    {
        foreach (var item in equiped)
        {
            if (upgradeHandlers.TryGetValue(item.Id, out var handler))
            {
                if (isAdd)
                {
                    handler.Upgrade(car);
                }
                else
                {
                    handler.Unupgrade(car);
                }
            }
        }
    }
}