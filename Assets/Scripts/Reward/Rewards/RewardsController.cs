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
        private DateTime _time = new DateTime();

        #endregion


        #region CodeLifeCycles

        public RewardsController(IUIController uiController, ITimer timer)
        {
            _uiController = uiController;
            _timer = timer;

            _timer.Tick += TickHandler;
            _timer.TimerFinish += TimerFinishHandler;

            _time.AddSeconds(10);
            _timer.StartTimer(_time.Second);

            var asd = new System.Collections.Generic.List<RewardData>();
            asd.Add(new RewardData { Currency = Currency.Coins, Day = 1, Value = 100 });

            _uiController.CreateRewards(asd);
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
            _uiController.SetRewardItemActive(1);
        }

        #endregion

    }
}