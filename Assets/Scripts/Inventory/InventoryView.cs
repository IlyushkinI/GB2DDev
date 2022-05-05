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
    private TMP_Dropdown _dropdownBase;

    [SerializeField]
    private RectTransform _dropdownBaseRectTransform;

    private float _dropdownBaseHeight;
    private Vector3 _dropdownBasePosition;

    private List<TMP_Dropdown> _dropdowns;

    #endregion


    #region Properies

    public bool isActive
    {
        set => _rootGameObject.gameObject.SetActive(value);
        get => _rootGameObject.gameObject.activeSelf;
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

    private void OnClickOK()
    {
        _eventsShed.Invoke(UIElements.ButtonOK);
    }

    private void OnClickCancel()
    {
        _eventsShed.Invoke(UIElements.ButtonCancel);
    }

    public void Display(IReadOnlyList<IItem> items)
    {
        _dropdowns = new List<TMP_Dropdown>(items.Count);

        _dropdownBasePosition = _dropdownBaseRectTransform.anchoredPosition3D;
        _dropdownBaseHeight = _dropdownBaseRectTransform.rect.height;

        for (int i = 0; i < items.Count; i++)
        {
            Debug.Log($"Id item: {items[i].Id}. Title item: {items[i].Info.Title}");
            
            _dropdowns.Add(GameObject.Instantiate(_dropdownBase, _dropdownBase.transform.parent));
            _dropdowns[i].gameObject.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(
                _dropdownBasePosition.x,
                _dropdownBasePosition.y + (_dropdownBaseHeight - _dropdownBasePosition.y) * -i,
                _dropdownBasePosition.z);

        }

        _dropdownBase.gameObject.SetActive(false);
    }

    #endregion

}
