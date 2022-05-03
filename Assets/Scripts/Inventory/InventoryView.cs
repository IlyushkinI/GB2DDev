using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryView : MonoBehaviour, IInventoryView
{

    [Space]
    [SerializeField]
    private Transform _rootGameObject;

    [Space]
    [SerializeField]
    private Button _buttonOK;

    [SerializeField]
    private Button _buttonCancel;

    public bool isActive
    {
        set => _rootGameObject.gameObject.SetActive(value);
        get => _rootGameObject.gameObject.activeSelf;
    }

    public void Display(IReadOnlyList<IItem> items)
    {
        foreach(var item in items)
            Debug.Log($"Id item: {item.Id}. Title item: {item.Info.Title}");
    }
}
