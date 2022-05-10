using TMPro;
using UnityEngine;


public class InventoryBaseItemView : MonoBehaviour
{

    #region Fields

    [SerializeField]
    private TextMeshProUGUI _label;

    [SerializeField]
    private TMP_Dropdown _dropdown;

    [Space]
    [SerializeField]
    private RectTransform _rectTransform;

    [Space]
    [SerializeField]
    private GlobalEventSO _eventSO;

    private Vector3 _anchoredPosition;
    private float _height;

    #endregion


    #region Properties

    public TextMeshProUGUI Label => _label;
    public TMP_Dropdown Dropdown => _dropdown;
    public Vector3 AnchoredPosition => _anchoredPosition;
    public float Height => _height;
    
    #endregion


    #region Unity Methods

    private void OnEnable()
    {
        _anchoredPosition = _rectTransform.anchoredPosition3D;
        _height = _rectTransform.rect.height;
        _dropdown.onValueChanged.AddListener(DropdownOnValueChanged);
    }

    private void OnDisable()
    {
        _dropdown.onValueChanged.RemoveAllListeners();
    }

    private void DropdownOnValueChanged(int value)
    {
        _eventSO?.Invoke(UIElements.Dropdown, value, Label.text);
    }

    #endregion
}
