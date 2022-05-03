using System.Collections.Generic;
using UnityEngine;


public class InventoryController : BaseController, IInventoryController
{
    private readonly IInventoryModel _inventoryModel;
    private readonly IInventoryView _inventoryView;
    private readonly IItemsRepository _itemsRepository;

    private readonly string _pathToView = "Prefabs/Shed";

    public InventoryController(List<ItemConfig> itemConfigs, InventoryModel inventoryModel, Transform placeForUI)
    {
        _inventoryModel = inventoryModel;
        _inventoryView = //new InventoryView();
        GameObject.Instantiate(Resources.Load<InventoryView>(_pathToView), placeForUI);
        _itemsRepository = new ItemsRepository(itemConfigs);
    }

    public void ShowInventory()
    {
        foreach (var item in _itemsRepository.Items.Values)
            _inventoryModel.EquipItem(item);

        var equippedItems = _inventoryModel.GetEquippedItems();

        _inventoryView.Display(equippedItems);
    }
}
