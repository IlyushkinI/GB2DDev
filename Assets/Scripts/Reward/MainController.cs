using System.Collections.Generic;


namespace Reward
{
    public sealed class MainController : BaseController
    {

        #region Fields

        private readonly UIController _uiController;
        private readonly RewardsController _rewardController;
        private readonly StorageController _storageController;
        private readonly GameButtonsController _gameButtonsController;

        #endregion


        #region CodeLifeCycles

        public MainController(UIEventSO eventsUI, RewardWindowView rewardWindow, List<CurrencySO> currencyList, ITimer timer, RewardsConfSO rewardsConfig)
        {
            _uiController = new UIController(rewardWindow, currencyList);
            AddController(_uiController);

            _storageController = new StorageController(_uiController);
            AddController(_storageController);

            _rewardController = new RewardsController(_uiController, timer, rewardsConfig, _storageController.StorageModel, eventsUI);
            AddController(_rewardController);

            _gameButtonsController = new GameButtonsController(eventsUI);
            AddController(_gameButtonsController);
        }

        #endregion

    }
}