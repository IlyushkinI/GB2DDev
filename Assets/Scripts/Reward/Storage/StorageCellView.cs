using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class StorageCellView : MonoBehaviour
{

    #region Fields

    [Space]
    [SerializeField]
    private Image _sprite;

    [SerializeField]
    private TextMeshProUGUI _value;

    #endregion


    #region Properties

    public Sprite Sprite { set => _sprite.sprite = value; }
    public int Value { set => _value.text = $"{value:D4}"; }

    #endregion

}
