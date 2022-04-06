using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace RaceMobile.Reward
{
    internal class SlotRewardView : MonoBehaviour
    {
        [SerializeField] private Image rewardIcon;
        [SerializeField] private TMP_Text daysText;
        [SerializeField] private TMP_Text countText;
        [SerializeField] private Image highlightImage;

        public void SetData(RewardModel reward, int dayNum, bool isSelected)
        {
            rewardIcon.sprite = reward.Icon;
            daysText.text = dayNum.ToString();
            countText.text = reward.Count.ToString();
            highlightImage.gameObject.SetActive(isSelected);
        }

    }
}