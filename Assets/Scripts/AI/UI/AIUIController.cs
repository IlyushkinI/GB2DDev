using System;
using UnityEngine;


namespace AI
{
    public sealed class AIUIController : BaseController, IObserver
    {

        #region Fields

        private readonly IAIUIView _uiView;
        private readonly AIUIEventSO _eventsUI;

        #endregion


        #region CodeLifeCycles

        public AIUIController(AIUIEventSO eventsUI, IAIUIView uiView)
        {
            _eventsUI = eventsUI;
            _uiView = uiView;
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
            Debug.Log("bf");
        }

        #endregion


        #region BaseController

        protected override void OnDispose()
        {
            _eventsUI.UIEvent -= UIEventHandler;
        }

        #endregion


        #region IObserver

        public void Catch(float data)
        {
            _uiView.EnemyPowerSet = data;
        }

        public void Catch(PlayerDataType dataType, IPlayerData data)
        {
            switch (dataType)
            {
                case PlayerDataType.Money:
                    _uiView.MoneySet = data.GetData(dataType);
                    break;
                case PlayerDataType.Health:
                    _uiView.HealthSet = data.GetData(dataType);
                    break;
                case PlayerDataType.Force:
                    _uiView.ForceSet = data.GetData(dataType);
                    break;
                default:
                    break;
            }
        }

        #endregion

    }
}