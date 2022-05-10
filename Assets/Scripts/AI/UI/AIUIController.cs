using System;
using UnityEngine;


namespace AI
{
    public sealed class AIUIController : BaseController, IObserver
    {

        #region Fields

        private readonly IAIUIView _uiView;
        private readonly AIUIEventSO _eventSO;

        private int _health;

        #endregion


        #region CodeLifeCycles

        public AIUIController(AIUIEventSO eventSO, IAIUIView uiView)
        {
            _eventSO = eventSO;
            _uiView = uiView;
            _eventSO.UIEvent += UIEventHandler;
        }

        #endregion


        #region Methods

        private void UIEventHandler(AIUIElements caller)
        {
            switch (caller)
            {
                case AIUIElements.None:
                    break;
                case AIUIElements.ButtonFight:
                    ButtonFightHandler();
                    break;
                default:
                    break;
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
            _eventSO.UIEvent -= UIEventHandler;
        }

        #endregion


        #region IObserver

        public void Catch(PlayerDataTypes dataType, IPlayerData data)
        {
            switch (dataType)
            {
                case PlayerDataTypes.Money:
                    _uiView.MoneySet = data.GetData(dataType);
                    break;
                case PlayerDataTypes.Health:
                    _uiView.HealthSet = data.GetData(dataType);
                    break;
                case PlayerDataTypes.Force:
                    _uiView.ForceSet = data.GetData(dataType);
                    break;
                default:
                    break;
            }
        }

        #endregion

    }
}