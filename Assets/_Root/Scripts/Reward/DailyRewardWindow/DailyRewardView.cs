using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

namespace RaceMobile.Reward
{
    internal class DailyRewardView : MonoBehaviour
    {
        [Header("Time Settings")]
        [SerializeField] public int TimeCooldown = 86400;
        [SerializeField] public int TimeDeadline = 172800;

        [Space]

        [Header("Reward Settings")]
        [SerializeField] public List<RewardModel> Rewards;

        [Space]

        [Header("UI")]
        [SerializeField] public TMP_Text RewardTimer;
        [SerializeField] public Button GetRewardButton;
        [SerializeField] public Button CloseWindowButton;
        [SerializeField] public Transform SlotsParent;
        [SerializeField] public SlotRewardView SlotPrefab;



        private void OnDestroy()
        {
            GetRewardButton.onClick.RemoveAllListeners();
        }

    }
}