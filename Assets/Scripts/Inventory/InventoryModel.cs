using System.Collections.Generic;

public class InventoryModel : IInventoryModel
{
    private readonly List<IItem> _items = new List<IItem>();

    public InventoryModel() { }

    public InventoryModel(List<IItem> items)
    {
        foreach (var item in items)
        {
            EquipItem(item);
        }
    }

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
}
