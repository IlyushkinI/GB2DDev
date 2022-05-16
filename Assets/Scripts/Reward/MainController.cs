using System.Collections.Generic;


namespace Reward
{
    public sealed class MainController : BaseController
    {

        #region Fields

        private readonly UIController _uiController;
        private readonly RewardsController _rewardController;

        #endregion


        #region CodeLifeCycles

        public MainController(UIEventSO eventsUI, RewardWindowView rewardWindow, List<CurrencySO> currencyList, ITimer timer)
        {
            _uiController = new UIController(rewardWindow, currencyList);
            AddController(_uiController);

            _rewardController = new RewardsController(_uiController, timer);
            AddController(_rewardController);
        }

        #endregion

        

    }
}