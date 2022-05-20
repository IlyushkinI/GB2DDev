using DOTween;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class InventoryView : MonoBehaviour, IInventoryView
{

    #region Fields

    [Space]
    [SerializeField]
    private Transform _rootGameObject;

    [Space]
    [SerializeField]
    private GlobalEventSO _eventsShed;

    [Space]
    [SerializeField]
    private Button _buttonOK;

    [SerializeField]
    private Button _buttonCancel;

    [Space]
    [SerializeField]
    private InventoryBaseItemView _baseItem;

    [Space]
    [SerializeField]
    private TextMeshProUGUI _textForEffect;

    [SerializeField]
    private DOTweenWindowView _window;

    private Dictionary<string, InventoryBaseItemView> _itemsList;

    #endregion


    #region Properies

    public bool isActive
    {
        set
        {
            _rootGameObject.gameObject.SetActive(value);
            if (value)
                _window.OpenWindow();
            else
                _window.CloseWindow();
        }

        get => _rootGameObject.gameObject.activeSelf;
    }

    public Dictionary<string, InventoryBaseItemView> ItemsList => _itemsList;

    public string SetTextForItemsEffect
    {
        set => _textForEffect.text = value;
    }

    #endregion


    #region Unity Methods

    private void OnEnable()
    {
        _buttonOK.onClick.AddListener(OnClickOK);
        _buttonCancel.onClick.AddListener(OnClickCancel);
    }

    private void OnDisable()
    {
        _buttonOK.onClick.RemoveAllListeners();
        _buttonCancel.onClick.RemoveAllListeners();
    }

    #endregion


    #region Methods

    public void MakeDropdownPanel(IReadOnlyList<IItem> items)
    {
        _itemsList = new Dictionary<string, InventoryBaseItemView>(items.Count);
        CreateItemsList(items, _baseItem, ref _itemsList);
    }

    private void OnClickOK()
    {
        _eventsShed.Invoke(UIElements.ButtonOK);
    }

    private void OnClickCancel()
    {
        _eventsShed.Invoke(UIElements.ButtonCancel);
    }

    private void CreateItemsList(IReadOnlyList<IItem> items, InventoryBaseItemView baseItem, ref Dictionary<string, InventoryBaseItemView> itemsList)
    {
        Vector3 baseItemPosition = baseItem.AnchoredPosition;
        float baseItemHeight = baseItem.Height;

        for (int i = 0; i < items.Count; i++)
        {
            var newItem = GameObject.Instantiate(_baseItem, _baseItem.transform.parent);

            newItem.gameObject.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(
                baseItemPosition.x,
                baseItemPosition.y + (baseItemHeight - baseItemPosition.y) * -i,
                baseItemPosition.z);

            newItem.Label.text = items[i].Info.Title;
            newItem.Dropdown.ClearOptions();

            itemsList.Add(items[i].Info.Title, newItem);
        }

        GameObject.Destroy(baseItem.gameObject);
    }

    #endregion

}
