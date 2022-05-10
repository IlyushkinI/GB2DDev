using UnityEngine;


namespace AI
{
    public sealed class AIUIController : BaseController, IObserver, IAIUIController
    {

        #region Fields

        private readonly IAIUIView _uiView;

        #endregion


        #region CodeLifeCycles

        public AIUIController(IAIUIView uiView)
        {
            _uiView = uiView;

            _uiView.EnableButtonGo = true;
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


        #region IAIUIController

        public bool EnableButtonGo { set => _uiView.EnableButtonGo = value; }

        public void Lose()
        {
            Debug.Log("LOSE");
        }

        public void Win()
        {
            Debug.Log("WIN");
        }

        #endregion

    }
}