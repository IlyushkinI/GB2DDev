using System.Collections.Generic;


public interface IItemsDatabase
{
    void AddItems(List<ItemConfig> items);
    void AddUpgradeItems(List<UpgradeItemConfig> upgradeItems);
    void GetByUpgradeItemName(string name, out UpgradeItemConfig upgradeItem, out ItemConfig item);
    List<UpgradeItemConfig> GetUpgradeItems(string title);
}
