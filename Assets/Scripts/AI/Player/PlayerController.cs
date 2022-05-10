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

        private void UIEventHandler(AIUIElements caller)
        {
            switch (caller)
            {
                case AIUIElements.ButtonHealthAdd:
                    if (_playerModel.TrySetData(PlayerDataTypes.Health, 1))
                    {
                        Notify(PlayerDataTypes.Health);
                    }
                    break;
                case AIUIElements.ButtonHealthSub:
                    if (_playerModel.TrySetData(PlayerDataTypes.Health, -1))
                    {
                        Notify(PlayerDataTypes.Health);
                    }
                    break;
                case AIUIElements.ButtonMoneyAdd:
                    if (_playerModel.TrySetData(PlayerDataTypes.Money, 1))
                    {
                        Notify(PlayerDataTypes.Money);
                    }
                    break;
                case AIUIElements.ButtonMoneyhSub:
                    if (_playerModel.TrySetData(PlayerDataTypes.Money, -1))
                    {
                        Notify(PlayerDataTypes.Money);
                    }
                    break;
                case AIUIElements.ButtonForceAdd:
                    if (_playerModel.TrySetData(PlayerDataTypes.Force, 1))
                    {
                        Notify(PlayerDataTypes.Force);
                    }
                    break;
                case AIUIElements.ButtonForceSub:
                    if (_playerModel.TrySetData(PlayerDataTypes.Force, -1))
                    {
                        Notify(PlayerDataTypes.Force);
                    }
                    break;
                default:
                    break;
            }
        }

        #endregion


        #region IPlayerObserver

        public void Notify(PlayerDataTypes dataType)
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
