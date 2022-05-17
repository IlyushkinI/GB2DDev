using System;


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
        private RewardData _currentReward;

        #endregion


        #region CodeLifeCycles

        public RewardsController(IUIController uiController, ITimer timer, RewardsConfSO rewardsConfig, IStorageModel storageModel)
        {
            _uiController = uiController;
            _rewardsConfig = rewardsConfig;
            _storageModel = storageModel;

            _timer = timer;
            _timer.Tick += TickHandler;
            _timer.TimerFinish += TimerFinishHandler;

            _uiController.CreateRewards(rewardsConfig.Rewards);
            MarkCollectedRewards();

            if (_storageModel.WhenCollectingAvailable == _zeroData)// if first start
            {
                _storageModel.CurrentRewardItem = -1;
                SetNextReward();
            }

            StartTimer();
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
            if (_time != _zeroData)
            {
                _time = _time.Subtract(_oneSecond);
                _uiController.SetTimer(_time);
            }
        }

        private void TimerFinishHandler()
        {
            _currentReward = _rewardsConfig.Rewards[_storageModel.CurrentRewardItem];

            _uiController.SetTimer(_zeroData);
            _uiController.SetRewardItemActive(_currentReward.Day);

            if (_storageModel.CurrentRewardItem < _rewardsConfig.Rewards.Count - 1)
            {
            _storageModel.SetCurrency(_currentReward.Currency, _storageModel.GetCurrency(_currentReward.Currency) + _currentReward.Value);
                SetNextReward();
                StartTimer();
            }
        }

        private void MarkCollectedRewards()
        {
            if (_storageModel.CurrentRewardItem != 0)
            {
                for (int i = 0; i < _storageModel.CurrentRewardItem; i++)
                {
                    _uiController.SetRewardItemActive(_rewardsConfig.Rewards[i].Day);
                }
            }
        }

        private void SetNextReward()
        {
            var privDay = _currentReward.Day;
            _storageModel.CurrentRewardItem++;
            _currentReward = _rewardsConfig.Rewards[_storageModel.CurrentRewardItem];
            _storageModel.WhenCollectingAvailable = DateTime.UtcNow.AddMinutes(_currentReward.Day - privDay);
            //_storageModel.CurrentItemDT = DateTime.UtcNow.AddDays(_currentReward.Day - asd);
        }

        private void StartTimer()
        {
            var timeSpan = _storageModel.WhenCollectingAvailable - DateTime.UtcNow;
            if (timeSpan < TimeSpan.Zero)
            {
                timeSpan = TimeSpan.Zero;
            }
            _time = _zeroData.Add(timeSpan);
            _timer.StartTimer(_time.Second);
        }

        #endregion

    }
}