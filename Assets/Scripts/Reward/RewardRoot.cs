using System.Collections.Generic;
using UnityEngine;


namespace Reward
{
    public class RewardRoot : MonoBehaviour
    {

        #region Fields

        [SerializeField]
        private UIEventSO _eventsUI;

        [SerializeField]
        private RewardWindowView _rewardWindow;

        [SerializeField]
        private List<CurrencySO> _currencyList;

        [Space]
        [SerializeField]
        private Timer _timer;

        [Space]
        [SerializeField]
        private RewardsConfSO _rewardsConfig;

        #endregion


        #region UnityMethods

        private void Awake()
        {
            _ = new MainController(_eventsUI, _rewardWindow, _currencyList, _timer, _rewardsConfig);
        }

        #endregion

    }
}