using System;


namespace Reward
{
    public sealed class RewardsController : BaseController
    {

        #region Fields

        private readonly TimeSpan _oneSecond = new TimeSpan(0, 0, 1);
        private readonly DateTime _zeroData = new DateTime();

        private readonly IUIController _uiController;
        private readonly ITimer _timer;
        private readonly RewardsConfSO _rewardsConfig;
        private readonly IStorageModel _storageModel;
        private readonly UIEventSO _eventsUI;
        private DateTime _timeToReward = new DateTime();

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

            if (_storageModel.WhenCollectingAvailable == _zeroData)
            {
                _storageModel.WhenCollectingAvailable = DateAfterX(_rewardsConfig.Rewards[0].Day);

            }

            StartTimer();
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
                    var currentReward = _rewardsConfig.Rewards[_storageModel.CurrentRewardItemID];
                    _uiController.SetRewardItemCollectedState(currentReward.Day, true);
                    _storageModel.SetCurrency(currentReward.Currency, _storageModel.GetCurrency(currentReward.Currency) + currentReward.Value);
                    _storageModel.CurrentRewardCollectedState = true;
                    _uiController.SetButtonGetRewardInteractable = false;
                    SetNextReward();
                    break;
                case UIElement.ButtonReset:
                    _storageModel.SetDefaults();
                    _storageModel.WhenCollectingAvailable = DateAfterX(_rewardsConfig.Rewards[0].Day);
                    _uiController.SetButtonGetRewardInteractable = false;
                    MarkCollectedRewards();
                    StartTimer();
                    break;
                default:
                    break;
            }
        }

        private void TickHandler()
        {
            if (_timeToReward != _zeroData)
            {
                _timeToReward = _timeToReward.Subtract(_oneSecond);
                _uiController.SetTimer(_timeToReward);
            }
        }

        private void TimerFinishHandler()
        {
            _uiController.SetTimer(_zeroData);
            _uiController.SetRewardItemActive(_rewardsConfig.Rewards[_storageModel.CurrentRewardItemID].Day, true);
            _uiController.SetRewardItemCollectedState(_rewardsConfig.Rewards[_storageModel.CurrentRewardItemID].Day, _storageModel.CurrentRewardCollectedState);
            if (!_storageModel.CurrentRewardCollectedState)
            {
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
            if (_storageModel.CurrentRewardItemID != (_rewardsConfig.Rewards.Count - 1))
            {
                var privDay = _rewardsConfig.Rewards[_storageModel.CurrentRewardItemID].Day;
                _storageModel.CurrentRewardCollectedState = false;
                _storageModel.CurrentRewardItemID++;
                _storageModel.WhenCollectingAvailable = DateAfterX(_rewardsConfig.Rewards[_storageModel.CurrentRewardItemID].Day - privDay);
                StartTimer();
            }
        }

        private DateTime DateAfterX(int value)
        {
            return DateTime.UtcNow.AddMinutes(value);
            //return DateTime.UtcNow.AddDays(value);
        }

        private void StartTimer()
        {
            var timeSpan = _storageModel.WhenCollectingAvailable - DateTime.UtcNow;
            if (timeSpan < TimeSpan.Zero)
            {
                timeSpan = TimeSpan.Zero;
            }
            _timeToReward = _zeroData.Add(timeSpan);
            _timer.StartTimer((int)timeSpan.TotalSeconds);
        }

        #endregion

    }
}