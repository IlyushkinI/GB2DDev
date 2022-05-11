using UnityEngine;


namespace AI
{
    public class BattleController : BaseController, IObserver
    {

        #region Fields

        private readonly BattleModel _battleModel;
        private readonly AIUIEventSO _eventsUI;
        private readonly IEnemyModel _enemyModel;
        private readonly IAIUIController _UI;

        #endregion


        #region CodeLifeCycles

        public BattleController(AIUIEventSO eventsUI, IEnemyModel enemyModel, IAIUIController UI)
        {
            _battleModel = new BattleModel();
            _eventsUI = eventsUI;
            _enemyModel = enemyModel;
            _UI = UI;
            _eventsUI.UIEvent += UIEventHandler;
        }

        #endregion


        #region Methods

        private void UIEventHandler(AIUIElement caller, PlayerDataType dataType, int data)
        {
            switch (caller)
            {
                case AIUIElement.ButtonFight:
                    ButtonFightHandler();
                    break;

                case AIUIElement.ButtonGo:
                    _UI.Win();
                    break;

            }
        }

        private void ButtonFightHandler()
        {
            if (_battleModel.IsPlayerWin(_enemyModel))
            {
                _UI.Win();
            }
            else
            {
                _UI.Lose();
            }
        }

        #endregion


        #region BaseController

        protected override void OnDispose()
        {
            _eventsUI.UIEvent -= UIEventHandler;
        }

        #endregion


        #region IObserver

        public void Catch(float _) { }

        public void Catch(PlayerDataType dataType, IPlayerData data)
        {
            if (dataType == PlayerDataType.CrimeLevel)
            {
                _UI.EnableButtonGo = !_battleModel.IsNeedBattle(data);
            }
        }

        #endregion

    }
}
