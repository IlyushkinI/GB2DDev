using System;
using TMPro;
using Tools;
using UnityEngine;


namespace Reward
{
    public sealed class RewardWindowView : MonoBehaviour
    {

        #region Fields

        private SubscriptionProperty<DateTime> _timeToReward = new SubscriptionProperty<DateTime>();

        [Space]
        [SerializeField]
        private RewardCellView _rewardCell;

        [SerializeField]
        private Transform _rewardsParent;

        [Space]
        [SerializeField]
        private StorageCellView _storageCell;

        [SerializeField]
        private Transform _storageParent;

        [Space]
        [SerializeField]
        private TextMeshProUGUI _textTimer;

        #endregion


        #region Properties

        public RewardCellView RewardCell => _rewardCell;
        public Transform RewardsParent => _rewardsParent;
        public StorageCellView StorageCell => _storageCell;
        public Transform StorageParent => _storageParent;
        public DateTime TimeToReward { set => _timeToReward.Value = value; }

        #endregion


        #region UnityMethods

        private void OnEnable()
        {
            _timeToReward.SubscribeOnChange(ChangeTimerView);
        }

        private void OnDisable()
        {
            _timeToReward.UnSubscriptionOnChange(ChangeTimerView);
        }

        #endregion


        #region Methods

        void ChangeTimerView(DateTime dateTime)
        {
            _textTimer.text = $"Time left - {_timeToReward.Value:T}";
        }

        #endregion

    }
}