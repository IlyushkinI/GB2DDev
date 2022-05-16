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
        private readonly DateTime _zeroData = new DateTime();
        private DateTime _time = new DateTime();

        #endregion


        #region CodeLifeCycles

        public RewardsController(IUIController uiController, ITimer timer, RewardsConfSO rewardsConfig)
        {
            _uiController = uiController;
            _timer = timer;
            
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
            _time = _zeroData;
            _uiController.SetTimer(_time);
            _uiController.SetRewardItemActive(1);
        }

        #endregion

    }
}