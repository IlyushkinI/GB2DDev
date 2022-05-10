using UnityEngine;


namespace AI
{
    public class BattleController : BaseController
    {

        #region Fields

        private readonly BattleModel _battleModel;
        private readonly AIUIEventSO _eventsUI;
        private readonly IEnemyModel _enemyModel;
        private readonly IPlayerData _playerModel;

        #endregion


        #region CodeLifeCycles

        public BattleController(AIUIEventSO eventsUI, IEnemyModel enemyModel, IPlayerData playerModel)
        {
            _battleModel = new BattleModel();
            _eventsUI = eventsUI;
            _enemyModel = enemyModel;
            _playerModel = playerModel;

            _eventsUI.UIEvent += UIEventHandler;
        }

        #endregion


        #region Methods

        private void UIEventHandler(AIUIElement caller, PlayerDataType dataType, int data)
        {
            if (caller == AIUIElement.ButtonFight)
            {
                ButtonFightHandler();
            }
        }

        private void ButtonFightHandler()
        {
            if (_battleModel.IsPlayerWin(_enemyModel))
            {
                Debug.Log("WIN");
            }
            else
            {
                Debug.Log("LOSE");
            }
        }

        #endregion


        #region BaseController

        protected override void OnDispose()
        {
            _eventsUI.UIEvent -= UIEventHandler;
        }

        #endregion

    }
}
