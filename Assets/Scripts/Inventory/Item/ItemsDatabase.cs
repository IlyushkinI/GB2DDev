using System.Collections.Generic;


public class ItemsDatabase : IItemsDatabase
{

    #region Fields

    private List<UpgradeItemConfig> _upgradeItems;
    private List<ItemConfig> _items;

    #endregion


    #region Properties

    public List<UpgradeItemConfig> UpgradeItems => _upgradeItems;
    public List<ItemConfig> Items => _items;

    #endregion


    #region CodeLifeCycles

    public ItemsDatabase(IEnumerable<UpgradeItemConfig> upgradeItems, IEnumerable<ItemConfig> items)
    {
        _upgradeItems = new List<UpgradeItemConfig>(upgradeItems);
        _items = new List<ItemConfig>(items);
    }

    #endregion


    #region IItemsDatabase

    public void AddItems(List<ItemConfig> items)
    {
        _items.AddRange(items);
    }

    public void AddUpgradeItems(List<UpgradeItemConfig> upgradeItems)
    {
        _upgradeItems.AddRange(upgradeItems);
    }

    public void GetByUpgradeItemName(string name, out UpgradeItemConfig upgradeItem, out ItemConfig item)
    {
        var uItem = _upgradeItems.Find(i => i.Name == name);

        upgradeItem = uItem;
        item = _items.Find(i => i.Id == uItem.Id);
    }

    public List<UpgradeItemConfig> GetUpgradeItems(string title)
    {
        int id = _items.Find(i => i.Title == title).Id;
        return _upgradeItems.FindAll(i => i.Id == id);
    }

    #endregion

}
