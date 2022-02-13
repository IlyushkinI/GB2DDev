using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InventoryView : BaseController, IInventoryView
{
    private readonly ResourcePath _viewInventoryPath = new ResourcePath { PathResource = "Prefabs/Inventory" };
    private readonly InventoryViewMonoBehavior _inventory;
    private readonly UpgradeHandlerRepository _upgradeRepository;
    private readonly IReadOnlyList<UpgradeItemConfig> _upgradeItems;
    private readonly ProfilePlayer _car;

    public InventoryView(Transform placeForUi, IReadOnlyList<UpgradeItemConfig> upgradeItems, UpgradeHandlerRepository upgradeRepository, ProfilePlayer car)
    {
        _car = car;
        _upgradeItems = upgradeItems;
        _upgradeRepository = upgradeRepository;
        _inventory = LoadInventoryView(placeForUi);
        _inventory.Init(CloseInventory);
        _inventory.InitUpgradeDetails(_upgradeItems);
    }
    public void InitInventory(MainMenuView view)
    {
        view.InitInventory(OpenInventory);
    }

    private InventoryViewMonoBehavior LoadInventoryView(Transform placeForUi)
    {
        var objectView = Object.Instantiate(ResourceLoader.LoadPrefab(_viewInventoryPath), placeForUi, false);
        objectView.active = false;
        AddGameObjects(objectView);

        return objectView.GetComponent<InventoryViewMonoBehavior>();
    }

    private void OpenInventory()
    {
        _inventory.gameObject.SetActive(true);
    }

    private void CloseInventory()
    {
        _inventory.gameObject.SetActive(false);
        _upgradeRepository.PopulateItems(_upgradeItems, _car);
    }

    public void Display(IReadOnlyList<IItem> items)
    {
        foreach(var item in items)
            Debug.Log($"Id item: {item.Id}. Title item: {item.Info.Title}");
    }

    protected override void OnDispose()
    {
        base.OnDispose();
        Object.Destroy(_inventory);
    }
}
