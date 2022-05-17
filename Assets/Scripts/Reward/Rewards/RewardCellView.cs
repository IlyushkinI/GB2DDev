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

    [SerializeField]
    private Image _rewardCollectedState;

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
            else
            {
                _rewardBackground.color = new Color(1, 1, 1, 100/255);
            }
        }
    }
    public bool SetCollectedState
    {
        set => _rewardCollectedState.color = new Color(0, 0, 0, value ? 0.5f : 0.0f);
    }

    #endregion

}
