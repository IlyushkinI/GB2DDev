﻿using System.Collections.Generic;
using UnityEngine;

public class InventoryController : BaseController, IInventoryController
{
    private readonly IInventoryModel _inventoryModel;
    private readonly IItemsRepository _itemsRepository;

    private IInventoryView _inventoryView;

    public InventoryController(List<ItemConfig> itemConfigs, InventoryModel inventoryModel)
    {
        _inventoryModel = inventoryModel;
        _itemsRepository = new ItemsRepository(itemConfigs);

        foreach (var item in _itemsRepository.Items.Values)
            _inventoryModel.EquipItem(item);
    }

    public void InitShedUI(Transform placeForUI, ResourcePath layoutPrefabPath, ResourcePath itemPrefabPath)
    {
        var view = GameObject.Instantiate(ResourceLoader.LoadPrefab(layoutPrefabPath), placeForUI);
        _inventoryView = view.GetComponent<IInventoryView>();
        _inventoryView.LoadShedUI(OnItemToggleChanged, _inventoryModel.GetAllItems(), itemPrefabPath);
    }

    private void OnItemToggleChanged(IItem item, bool isOn)
    {
        if (isOn)
            _inventoryModel.EquipItem(item);
        else
            _inventoryModel.UnEquipItem(item);
    }

    public void ShowInventory()
    {
        if (!_inventoryModel.IsInShed)
        {
            var equippedItems = _inventoryModel.GetEquippedItems();
            _inventoryView.Display(equippedItems);
        }
        else
        {
            _inventoryView.Show();
        }
    }
}
