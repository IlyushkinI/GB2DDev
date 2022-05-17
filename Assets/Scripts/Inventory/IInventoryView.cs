using System.Collections.Generic;

public interface IInventoryView
{
    void MakeDropdownPanel(IReadOnlyList<IItem> items);
}
