using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class InventoryController : BaseController, IInventoryController
{

    #region Fields

    private readonly InventoryView _inventoryView;
    private readonly IItemsRepository _itemsRepository;
    IReadOnlyList<UpgradeItemConfig> _availableUpgradeItems;
    List<UpgradeItemConfig> _applyUpgradeItems;

    private readonly string _pathToView = "Prefabs/Shed";
    private readonly GlobalEventSO _eventsShed;

    private InventoryModel _inventoryModel;

    private Car _car;

    private float _currentSpeed;
    private float _defaultSpeed;
    private float _currentControl;
    private float _defaultControl;

    private string _emptyDropdownLabel = "None";

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
        _applyUpgradeItems = new List<UpgradeItemConfig>();
        _availableUpgradeItems = upgradeItems;
        _itemsRepository = new ItemsRepository(itemConfigs);
        _inventoryModel = new InventoryModel(new List<IItem>(_itemsRepository.Items.Values));

        _eventsShed = eventsShed;

        _inventoryView = GameObject.Instantiate(Resources.Load<InventoryView>(_pathToView), placeForUI);
        _inventoryView.MakeDropdownPanel(EquipedItems);

        ConfigureDropdownPanel(_availableUpgradeItems, _inventoryModel, _inventoryView);

        _inventoryView.isActive = false;

        _eventsShed.GlobalEventAction += EventUIHandler;
        _eventsShed.GlobalEventDropdown += EventUIHandler;
    }

    #endregion


    #region Methods

    public void ShowInventory(Car car)
    {
        _car = car;
        _currentSpeed = _car.Speed;
        _defaultSpeed = _car.Speed;
        _currentControl = _car.Control;
        _defaultControl = _car.Control;
        _inventoryView.isActive = true;
        //ActualizeDropdownPanel(_car.AppliedItems);
        UpdateInventoryText();
    }

    protected override void OnDispose()
    {
        _eventsShed.GlobalEventAction -= EventUIHandler;
        _eventsShed.GlobalEventDropdown -= EventUIHandler;
    }

    private void UpdateInventoryText()
    {
        _inventoryView.SetTextForItemsEffect =
        "Effects:\n" +
            $"\tCar speed : {FormatText(_defaultSpeed, _currentSpeed)}\n" +
            $"\tCar control : {FormatText(_defaultControl, _currentControl)}";
    }

    private string FormatText(float a, float b)
    {
        if (Mathf.Approximately(b - a, 0.0f))
        {
            return $"{a}";
        }
        else
        {
            if ((b - a) > 0.0f)
            {
                return $"{a} + {b - a}";
            }
            else
            {
                return $"{a} - {a - b}";
            }
        }
    }

    private void ConfigureDropdownPanel(IReadOnlyList<UpgradeItemConfig> upgradeItems, InventoryModel inventoryModel, InventoryView inventoryView)
    {
        foreach (var item in inventoryModel.GetEquippedItems())
        {
            inventoryView.ItemsList[item.Info.Title].Dropdown.options.Add(new TMP_Dropdown.OptionData(_emptyDropdownLabel));

            foreach (var upgradeItem in upgradeItems)
            {
                if (upgradeItem.Id == item.Id)
                {
                    inventoryView.ItemsList[item.Info.Title].Dropdown.options.Add(new TMP_Dropdown.OptionData(upgradeItem.Name));
                }
            }
        }
    }

    private void ActualizeDropdownPanel(List<UpgradeItemConfig> itemConfigs)
    {
        foreach (var item in itemConfigs)
        {
            
        }
    }

    private void EventUIHandler(UIElements caller)
    {
        switch (caller)
        {
            case UIElements.None:
                break;
            case UIElements.ButtonOK:
                _car.AppliedItems = _applyUpgradeItems;
                _inventoryView.isActive = false;
                _eventsShed.Invoke(UIElements.ExitShed);
                break;
            case UIElements.ButtonCancel:
                _inventoryView.isActive = false;
                _eventsShed.Invoke(UIElements.ExitShed);
                break;
        }
    }

    private void EventUIHandler(UIElements caller, int value, string dropdownLabel)
    {
        ManageApplyList(value, dropdownLabel);
        ApplyItems();
        UpdateInventoryText();
    }

    private void ManageApplyList(int value, string dropdownLabel)
    {
        foreach (var itemAvailable in _availableUpgradeItems)
        {
            if (_inventoryView.ItemsList[dropdownLabel].Dropdown.options[value].text == _emptyDropdownLabel)
            {
                _applyUpgradeItems.Remove(GetItemByID(_applyUpgradeItems, GetIDbyName(EquipedItems, dropdownLabel)));
                break;
            }

            if (_inventoryView.ItemsList[dropdownLabel].Dropdown.options[value].text == itemAvailable.Name)
            {
                _applyUpgradeItems.Remove(GetItemByID(_applyUpgradeItems, itemAvailable.Id));
                _applyUpgradeItems.Add(itemAvailable);
                break;
            }
        }
    }

    private void ApplyItems()
    {
        _currentSpeed = _defaultSpeed;
        _currentControl = _defaultControl;

        foreach (var item in _applyUpgradeItems)
        {
            switch (item.UpgradeType)
            {
                case UpgradeType.None:
                    break;
                case UpgradeType.Speed:
                    _currentSpeed += item.ValueUpgrade;
                    break;
                case UpgradeType.Control:
                    _currentControl += item.ValueUpgrade;
                    break;
            }
        }
    }

    private int GetIDbyName(IReadOnlyList<IItem> list, string name)
    {
        foreach (var item in list)
        {
            if (item.Info.Title == name)
            {
                return item.Id;
            }
        }
        return -1;
    }

    private UpgradeItemConfig GetItemByID(List<UpgradeItemConfig> applyUpgradeItems, int ID)
    {
        foreach (var item in applyUpgradeItems)
        {
            if (item.Id == ID)
            {
                return item;
            }
        }
        return null;
    }

    #endregion

}
