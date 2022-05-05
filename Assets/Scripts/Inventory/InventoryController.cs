using System.Collections.Generic;
using UnityEngine;


public class InventoryController : BaseController, IInventoryController
{

    #region Fields

    private readonly IInventoryModel _inventoryModel;
    private readonly InventoryView _inventoryView;
    private readonly IItemsRepository _itemsRepository;

    private readonly string _pathToView = "Prefabs/Shed";
    private readonly GlobalEventSO _eventsShed;

    #endregion


    #region CodeLifeCycles

    public InventoryController(List<ItemConfig> itemConfigs, InventoryModel inventoryModel, Transform placeForUI, GlobalEventSO eventsShed)
    {
        _eventsShed = eventsShed;

        _inventoryModel = inventoryModel;
        _inventoryView = GameObject.Instantiate(Resources.Load<InventoryView>(_pathToView), placeForUI);
        _inventoryView.isActive = false;

        _itemsRepository = new ItemsRepository(itemConfigs);
        _eventsShed.GlobalEventAction += EventUIHandler;
    }

    #endregion


    #region Methods

    public void ShowInventory()
    {
        _inventoryView.isActive = true;

        foreach (var item in _itemsRepository.Items.Values)
            _inventoryModel.EquipItem(item);

        var equippedItems = _inventoryModel.GetEquippedItems();

        _inventoryView.Display(equippedItems);
    }

    protected override void OnDispose()
    {
        _eventsShed.GlobalEventAction -= EventUIHandler;
    }

    private void EventUIHandler(UIElements caller)
    {
        switch (caller)
        {
            case UIElements.None:
                break;
            case UIElements.ButtonOK:
                _inventoryView.isActive = false;
                break;
            case UIElements.ButtonCancel:
                _inventoryView.isActive = false;
                break;
        }
    }

    #endregion

}