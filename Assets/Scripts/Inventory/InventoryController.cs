using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class InventoryController : BaseController, IInventoryController
{

    #region Fields

    private readonly InventoryView _inventoryView;
    private readonly IItemsRepository _itemsRepository;

    private readonly string _pathToView = "Prefabs/Shed";
    private readonly GlobalEventSO _eventsShed;

    private InventoryModel _inventoryModel;

    #endregion


    #region Properties

    public IReadOnlyList<IItem> EquipedItems => _inventoryModel.GetEquippedItems();

    #endregion


    #region CodeLifeCycles

    public InventoryController(
        List<ItemConfig> itemConfigs,
        IReadOnlyList<UpgradeItemConfig> upgradeItems,
        Transform placeForUI,
        GlobalEventSO eventsShed)
    {
        _itemsRepository = new ItemsRepository(itemConfigs);
        _inventoryModel = new InventoryModel(new List<IItem>(_itemsRepository.Items.Values));

        _eventsShed = eventsShed;

        _inventoryView = GameObject.Instantiate(Resources.Load<InventoryView>(_pathToView), placeForUI);
        _inventoryView.Display(_inventoryModel.GetEquippedItems());

        foreach (var item in _inventoryModel.GetEquippedItems())
        {
            _inventoryView.ItemsList[item.Info.Title].options.Add(new TMP_Dropdown.OptionData("None"));

            foreach (var upgradeItem in upgradeItems)
            {
                if (upgradeItem.Id == item.Id)
                {
                    _inventoryView.ItemsList[item.Info.Title].options.Add(new TMP_Dropdown.OptionData(upgradeItem.name));
                }
            }
        }

        _inventoryView.isActive = false;

        _eventsShed.GlobalEventAction += EventUIHandler;
    }

    #endregion


    #region Methods

    public void ShowInventory(float speed, float control)
    {
        _inventoryView.isActive = true;
        _inventoryView.Display(_inventoryModel.GetEquippedItems());
        _inventoryView.SetTextForItemsEffect =
            "Effects:\n" +
            $"\tCar speed : {speed}\n" +
            $"\tCar control : {control}";
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