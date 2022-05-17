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
        private readonly UIEventSO _eventsUI;
        private readonly DateTime _zeroData = new DateTime();
        private DateTime _time = new DateTime();
        private RewardData _currentReward;

        #endregion


        #region CodeLifeCycles

        public RewardsController(IUIController uiController, ITimer timer, RewardsConfSO rewardsConfig, IStorageModel storageModel, UIEventSO eventsUI)
        {
            _uiController = uiController;
            _rewardsConfig = rewardsConfig;
            _storageModel = storageModel;

            _eventsUI = eventsUI;
            _eventsUI.UIEvent += UIEventHandler;

            _timer = timer;
            _timer.Tick += TickHandler;
            _timer.TimerFinish += TimerFinishHandler;

            _uiController.CreateRewards(_rewardsConfig.Rewards);
            MarkCollectedRewards();

            if (_storageModel.WhenCollectingAvailable == _zeroData)// if first start
            {
                _storageModel.CurrentRewardItemID = -1;
                SetNextReward();
            }
            else
            {
                StartTimer();
            }
        }

        protected override void OnDispose()
        {
            _timer.Tick -= TickHandler;
            _timer.TimerFinish -= TimerFinishHandler;
            _eventsUI.UIEvent -= UIEventHandler;
        }

        #endregion


        #region Methods

        private void UIEventHandler(UIElement caller)
        {
            switch (caller)
            {
                case UIElement.ButtonGetReward:
                    _uiController.SetRewardItemCollectedState(_currentReward.Day, true);
                    _storageModel.SetCurrency(_currentReward.Currency, _storageModel.GetCurrency(_currentReward.Currency) + _currentReward.Value);
                    //if ()
                    SetNextReward();
                    break;
                case UIElement.ButtonReset:
                    _storageModel.SetDefaults();
                    _currentReward = new RewardData();
                    MarkCollectedRewards();
                    SetNextReward();
                    break;
                default:
                    break;
            }
        }

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
            _currentReward = _rewardsConfig.Rewards[_storageModel.CurrentRewardItemID];

            _uiController.SetTimer(_zeroData);

            if (_storageModel.CurrentRewardItemID < _rewardsConfig.Rewards.Count)
            {
                _uiController.SetRewardItemActive(_currentReward.Day, true);
                _uiController.SetButtonGetRewardInteractable = true;
            }
        }

        private void MarkCollectedRewards()
        {
            for (int i = 0; i < _rewardsConfig.Rewards.Count; i++)
            {
                if (i < _storageModel.CurrentRewardItemID)
                {
                    _uiController.SetRewardItemActive(_rewardsConfig.Rewards[i].Day, true);
                    _uiController.SetRewardItemCollectedState(_rewardsConfig.Rewards[i].Day, true);
                }
                else
                {
                    _uiController.SetRewardItemActive(_rewardsConfig.Rewards[i].Day, false);
                    _uiController.SetRewardItemCollectedState(_rewardsConfig.Rewards[i].Day, false);
                }
            }
        }

        private void SetNextReward()
        {
            _uiController.SetButtonGetRewardInteractable = false;

            if (_storageModel.CurrentRewardItemID != (_rewardsConfig.Rewards.Count - 1))
            {
                var privDay = _currentReward.Day;
                _storageModel.CurrentRewardItemID++;
                _currentReward = _rewardsConfig.Rewards[_storageModel.CurrentRewardItemID];
                _storageModel.WhenCollectingAvailable = DateTime.UtcNow.AddMinutes(_currentReward.Day - privDay);
                //_storageModel.CurrentItemDT = DateTime.UtcNow.AddDays(_currentReward.Day - asd);
                StartTimer();
            }
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