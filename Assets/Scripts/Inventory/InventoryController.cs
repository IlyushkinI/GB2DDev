using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class InventoryController : BaseController, IInventoryController
{

    #region Fields

    private InventoryView _inventoryView;
    private readonly IItemsRepository _itemsRepository;
    List<UpgradeItemConfig> _applyUpgradeItems;

    private readonly GlobalEventSO _eventsShed;
    private readonly AsyncOperationHandle _sheedPrefab;
    private InventoryModel _inventoryModel;

    private Car _car;

    private float _currentSpeed;
    private float _defaultSpeed;
    private float _currentControl;
    private float _defaultControl;

    private string _emptyDropdownLabel = "None";

    private ItemsDatabase _itemsDatabase;

    #endregion


    #region Properties

    public IReadOnlyList<IItem> EquipedItems => _inventoryModel.GetEquippedItems();

    #endregion


    #region CodeLifeCycles

    public InventoryController(
        List<ItemConfig> itemConfigs,
        IReadOnlyList<UpgradeItemConfig> upgradeItems,
        Transform placeForUI,
        GlobalEventSO eventsShed,
        AssetReference sheedPrefab)
    {
        _itemsRepository = new ItemsRepository(itemConfigs);
        _inventoryModel = new InventoryModel(new List<IItem>(_itemsRepository.Items.Values));
        _itemsDatabase = new ItemsDatabase(upgradeItems, itemConfigs);

        _eventsShed = eventsShed;

        //_inventoryView = GameObject.Instantiate(Resources.Load<InventoryView>(_pathToView), placeForUI);
        //AddGameObjects(_inventoryView.gameObject);
        _sheedPrefab = Addressables.InstantiateAsync(sheedPrefab, placeForUI);
        _sheedPrefab.Completed += AddressableLoadHandler;

        _eventsShed.GlobalEventAction += EventUIHandler;
        _eventsShed.GlobalEventDropdown += EventUIHandler;
    }

    private void AddressableLoadHandler(AsyncOperationHandle obj)
    {
        _inventoryView = ((GameObject)obj.Result).GetComponent<InventoryView>();
        _inventoryView.MakeDropdownPanel(EquipedItems);

        ConfigureDropdownPanel(_itemsDatabase, _inventoryView);

        _inventoryView.isActive = false;
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
        _applyUpgradeItems = new List<UpgradeItemConfig>(_car.AppliedItems);
        _inventoryView.isActive = true;
        SetValuesForDropdownPanel(_car.AppliedItems, _itemsDatabase, _inventoryView);
        UpdateInventoryText();
    }

    protected override void OnDispose()
    {
        _eventsShed.GlobalEventAction -= EventUIHandler;
        _eventsShed.GlobalEventDropdown -= EventUIHandler;
        Addressables.ReleaseInstance(_sheedPrefab);
    }

    private void UpdateInventoryText()
    {
        _inventoryView.SetTextForItemsEffect =
        "Effects:\n" +
            $"\tCar speed : {FormatText(_defaultSpeed, _currentSpeed)}\n" +
            $"\tCar control : {FormatText(_defaultControl, _currentControl)}";
    }

    private string FormatText(float defValue, float curValue)
    {
        if (Mathf.Approximately(curValue - defValue, 0.0f))
        {
            return $"{defValue}";
        }
        else
        {
            if (curValue > defValue)
            {
                return $"{defValue} + {curValue - defValue}";
            }
            else
            {
                return $"{defValue} - {defValue - curValue}";
            }
        }
    }

    private void ConfigureDropdownPanel(ItemsDatabase itemsDatabase, InventoryView inventoryView)
    {
        foreach (var item in itemsDatabase.Items)
        {
            inventoryView.ItemsList[item.Title].Dropdown.options.Add(new TMP_Dropdown.OptionData(_emptyDropdownLabel));
            inventoryView.ItemsList[item.Title].Dropdown.options.AddRange(itemsDatabase.GetUpgradeItems(item.Title).ConvertAll(i => new TMP_Dropdown.OptionData(i.Name)));
        }
    }

    private void SetValuesForDropdownPanel(List<UpgradeItemConfig> itemsOnPlayer, ItemsDatabase itemsDatabase, InventoryView inventoryView)
    {
        foreach (var item in itemsDatabase.Items)
        {
            var itemOnPlayer = itemsOnPlayer.Find(i => i.Id == item.Id);
            int index = 0;

            if (itemOnPlayer != null)
            {
                index = inventoryView.ItemsList[item.Title].Dropdown.options.FindIndex(i => i.text == itemOnPlayer.Name);
            }

            inventoryView.ItemsList[item.Title].Dropdown.SetValueWithoutNotify(index);
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

    private void EventUIHandler(UIElements _, int dropdownValue, string dropdownItemLabel)
    {
        ManageApplyList(dropdownValue, dropdownItemLabel, _inventoryView, _applyUpgradeItems, _itemsDatabase, _emptyDropdownLabel);
        UpdateInventoryText();
    }

    private void ManageApplyList(
        int dropdownValue,
        string dropdownItemLabel,
        InventoryView inventoryView,
        List<UpgradeItemConfig> applyUpgradeItems,
        ItemsDatabase itemsDatabase,
        string emptyDropdownLabel)
    {
        var dropdownValueName = inventoryView.ItemsList[dropdownItemLabel].Dropdown.options[dropdownValue].text;// uItem.Name

        var upgradeItemForRemove = applyUpgradeItems.Find(i => i.Id == itemsDatabase.Items.Find(ii => ii.Title == dropdownItemLabel).Id);// uItem or null
        var upgradeItemForAdd = itemsDatabase.UpgradeItems.Find(i => i.Name == dropdownValueName);// uItem or null

        if (dropdownValueName == emptyDropdownLabel)
        {
            ApplyItemForUI(false, upgradeItemForRemove);
            applyUpgradeItems.Remove(upgradeItemForRemove);
        }
        else
        {
            if (upgradeItemForRemove != null)
            {
                ApplyItemForUI(false, upgradeItemForRemove);
                applyUpgradeItems.Remove(upgradeItemForRemove);
            }

            ApplyItemForUI(true, upgradeItemForAdd);
            applyUpgradeItems.Add(upgradeItemForAdd);
        }
    }

    private void ApplyItemForUI(bool isIncrease, UpgradeItemConfig item)
    {
        switch (item.UpgradeType)
        {
            case UpgradeType.None:
                break;
            case UpgradeType.Speed:
                _currentSpeed += item.ValueUpgrade * (isIncrease ? 1 : -1);
                break;
            case UpgradeType.Control:
                _currentControl += item.ValueUpgrade * (isIncrease ? 1 : -1);
                break;
        }
    }

    #endregion

}
