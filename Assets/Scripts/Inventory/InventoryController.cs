using System.Collections.Generic;
using UnityEngine;

public class InventoryController : BaseController, IInventoryController
{

    private readonly IInventoryModel _inventoryModel;
    private readonly IInventoryView _inventoryView;
    private readonly IItemsRepository _itemsRepository;
    

    public InventoryController(List<ItemConfig> itemConfigs, InventoryModel inventoryModel, Transform placeForUi, IReadOnlyList<UpgradeItemConfig> upgradeItems, UpgradeHandlerRepository upgradeRepository, ProfilePlayer car)
    {

        _inventoryModel = inventoryModel;
        _inventoryView = new InventoryView(placeForUi, upgradeItems, upgradeRepository, car);
        _itemsRepository = new ItemsRepository(itemConfigs);
        
    }

    public void InitInventory(MainMenuView view)
    {
        _inventoryView.InitInventory(view);
    }

    public void ShowInventory()
    {
        foreach (var item in _itemsRepository.Items.Values)
            _inventoryModel.EquipItem(item);

        var equippedItems = _inventoryModel.GetEquippedItems();
        _inventoryView.Display(equippedItems);
    }


}
