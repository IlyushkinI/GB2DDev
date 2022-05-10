using System.Collections.Generic;


namespace AI
{
    public sealed class PlayerController : BaseController, IObservable
    {

        #region Fields

        private readonly PlayerView _playerView;
        private readonly PlayerModel _playerModel;
        private readonly AIUIEventSO _eventSO;

        private List<IObserver> _observers;

        #endregion


        #region CodeLifeCycles

        public PlayerController(PlayerView playerView, AIUIEventSO eventSO)
        {
            _playerView = playerView;
            _eventSO = eventSO;

            _playerModel = new PlayerModel();

            _observers = new List<IObserver>();

            _eventSO.UIEvent += UIEventHandler;
        }

        #endregion


        #region Methods

        private void UIEventHandler(AIUIElement caller, PlayerDataType dataType, int data)
        {
            if (AIUIElement.ConfigButtons.HasFlag(caller))
                if (_playerModel.TrySetData(dataType, data))
                {
                    UnityEngine.Debug.Log($"Button {caller}. Data {dataType}. Value {data}.");
                    Notify(dataType);
                }


            //switch (caller)
            //{
            //    case AIUIElement.ButtonHealthAdd:
            //        if (_playerModel.TrySetData(PlayerDataType.Health, 1))
            //        {
            //            Notify(PlayerDataType.Health);
            //        }
            //        break;
            //    case AIUIElement.ButtonHealthSub:
            //        if (_playerModel.TrySetData(PlayerDataType.Health, -1))
            //        {
            //            Notify(PlayerDataType.Health);
            //        }
            //        break;
            //    case AIUIElement.ButtonMoneyAdd:
            //        if (_playerModel.TrySetData(PlayerDataType.Money, 1))
            //        {
            //            Notify(PlayerDataType.Money);
            //        }
            //        break;
            //    case AIUIElement.ButtonMoneyhSub:
            //        if (_playerModel.TrySetData(PlayerDataType.Money, -1))
            //        {
            //            Notify(PlayerDataType.Money);
            //        }
            //        break;
            //    case AIUIElement.ButtonForceAdd:
            //        if (_playerModel.TrySetData(PlayerDataType.Force, 1))
            //        {
            //            Notify(PlayerDataType.Force);
            //        }
            //        break;
            //    case AIUIElement.ButtonForceSub:
            //        if (_playerModel.TrySetData(PlayerDataType.Force, -1))
            //        {
            //            Notify(PlayerDataType.Force);
            //        }
            //        break;
            //    default:
            //        break;
            //}

        }

        #endregion


        #region IPlayerObserver

        public void Notify(PlayerDataType dataType)
        {
            _observers.ForEach(i => i.Catch(dataType, _playerModel));
        }

        public void Subscribe(IObserver enemy)
        {
            if (!_observers.Contains(enemy))
            {
                _observers.Add(enemy);
            }
        }

        public void Unsubscribe(IObserver enemy)
        {
            _observers.Remove(enemy);
        }

        #endregion


        #region BaseController

        protected override void OnDispose()
        {
            _eventSO.UIEvent -= UIEventHandler;
        }

        #endregion

    }
}
