using System;
using UnityEngine;


namespace Reward
{
    public sealed class RewardsController : BaseController
    {

        #region Fields

        private readonly TimeSpan _oneSecond = new TimeSpan(0, 0, 1);
        private readonly IUIController _uiController;
        private readonly ITimer _timer;
        private readonly RewardsConfSO _rewardsConfig;
        private readonly IStorageModel _storageModel;
        private readonly DateTime _zeroData = new DateTime();
        private DateTime _time = new DateTime();


        #endregion


        #region CodeLifeCycles

        public RewardsController(IUIController uiController, ITimer timer, RewardsConfSO rewardsConfig, IStorageModel storageModel)
        {
            _uiController = uiController;
            _timer = timer;
            _rewardsConfig = rewardsConfig;
            _storageModel = storageModel;

            _time = _time.AddSeconds(10);
            _timer.StartTimer(_time.Second);

            _uiController.CreateRewards(rewardsConfig.Rewards);

            _timer.Tick += TickHandler;
            _timer.TimerFinish += TimerFinishHandler;
        }

        protected override void OnDispose()
        {
            _timer.Tick -= TickHandler;
            _timer.TimerFinish -= TimerFinishHandler;
        }

        #endregion


        #region Methods

        private void TickHandler()
        {
            _time = _time.Subtract(_oneSecond);
            _uiController.SetTimer(_time);
        }

        private void TimerFinishHandler()
        {
            var currentReward = _rewardsConfig.Rewards[_storageModel.CurrentRewardItem];

            _time = _zeroData;
            _uiController.SetTimer(_time);
            _uiController.SetRewardItemActive(currentReward.Day);

            _storageModel.SetCurrency(currentReward.Currency, _storageModel.GetCurrency(currentReward.Currency) + currentReward.Value);
            
            if (_storageModel.CurrentRewardItem < _rewardsConfig.Rewards.Count - 1)
            {
                _storageModel.CurrentRewardItem++;
                _time = _time.AddSeconds(10);
                _timer.StartTimer(_time.Second);
            }
        }

        #endregion

    }
}