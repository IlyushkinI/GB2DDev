using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ShedView : MonoBehaviour
{
    [SerializeField] private Button _buttonExitShed;
    [SerializeField] private Dropdown _dropdownItems;

    public void ExitShed(UnityAction enterShed)
    {
        _buttonExitShed.onClick.AddListener(enterShed);
    }

    public void DropdownItems(IReadOnlyList<IItem> items)
    {
        var itemTitle = new List<string>();
        
        foreach (var item in items)
        {
            itemTitle.Add(item.Info.Title);
        }

        _dropdownItems.AddOptions(itemTitle);
    }

    protected void OnDestroy()
    {
        _buttonExitShed.onClick.RemoveAllListeners();
    }
}
