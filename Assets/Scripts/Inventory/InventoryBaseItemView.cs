using TMPro;
using UnityEngine;


public class InventoryBaseItemView : MonoBehaviour
{

    #region Fields

    [SerializeField]
    private TextMeshProUGUI _text;

    [SerializeField]
    private TMP_Dropdown _dropdownBase;

    [Space]
    [SerializeField]
    private RectTransform _rectTransform;

    private Vector3 _anchoredPosition;
    private float _height;

    #endregion


    #region Properties

    public TextMeshProUGUI Label => _text;
    public TMP_Dropdown Dropdown => _dropdownBase;
    public Vector3 AnchoredPosition => _anchoredPosition;
    public float Height => _height;

    #endregion


    #region Unity Methods

    private void OnEnable()
    {
        _anchoredPosition = _rectTransform.anchoredPosition3D;
        _height = _rectTransform.rect.height;
    }

    #endregion
}
