using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace RaceMobile.Reward
{
    internal class DailyRewardView : MonoBehaviour
    {
        [Header("UI")]
        [SerializeField] public TMP_Text RewardTimer;
        [SerializeField] public Button GetRewardButton;
        [SerializeField] public Transform SlotsParent;
        [SerializeField] public SlotRewardView SlotPrefab;
    }
}