using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class RewardCellView : MonoBehaviour
{

    #region Fields

    [Space]
    [SerializeField]
    private Image _sprite;

    [SerializeField]
    private TextMeshProUGUI _daysToGet;

    [SerializeField]
    private TextMeshProUGUI _value;

    [Space]
    [SerializeField]
    private Image _rewardBackground;

    [SerializeField]
    private Color _rewardActiveColor;

    #endregion


    #region Properties

    public Sprite Sprite { set => _sprite.sprite = value; }
    public int Day { set => _daysToGet.text = $"Day {value}"; }
    public int Value { set => _value.text = $"{value}"; }
    public bool ActivateReward
    {
        set
        {
            if (value)
            {
                _rewardBackground.color = _rewardActiveColor;
            }
        }
    }

    #endregion

}
