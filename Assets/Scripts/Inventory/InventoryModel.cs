using System.Collections.Generic;


public class InventoryModel : IInventoryModel
{

    #region Fields

    private readonly List<IItem> _items = new List<IItem>();

    #endregion


    #region CodeLifeCycles

    public InventoryModel() { }

    public InventoryModel(List<IItem> items)
    {
        foreach (var item in items)
        {
            EquipItem(item);
        }
    }

    #endregion


    #region IInventoryModel

    public IReadOnlyList<IItem> GetEquippedItems()
    {
        return _items;
    }

    public void EquipItem(IItem item)
    {
        if (!_items.Contains(item))
        {
            _items.Add(item);
        }
    }

    public void UnEquipItem(IItem item)
    {
        _items.Remove(item);
    }

    #endregion

}
