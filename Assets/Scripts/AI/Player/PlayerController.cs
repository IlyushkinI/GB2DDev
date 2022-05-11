using System.Collections.Generic;


namespace AI
{
    public sealed class PlayerController : BaseController, IObservable, IPlayerController
    {

        #region Fields

        private readonly PlayerView _playerView;
        private readonly PlayerModel _playerModel;
        private readonly AIUIEventSO _eventsUI;

        private List<IObserver> _observers;

        #endregion


        #region CodeLifeCycles

        public PlayerController(PlayerView playerView, AIUIEventSO eventsUI)
        {
            _playerView = playerView;
            _eventsUI = eventsUI;

            _playerModel = new PlayerModel();
            
            _observers = new List<IObserver>();

            _eventsUI.UIEvent += UIEventHandler;
        }

        #endregion


        #region Methods

        private void UIEventHandler(AIUIElement caller, PlayerDataType dataType, int data)
        {
            if (AIUIElement.ConfigElements.HasFlag(caller))
            {
                if (_playerModel.TrySetData(dataType, data))
                {
                    Notify(dataType);
                }
            }
        }

        #endregion


        #region IPlayerController

        public IPlayerData GetPlayerModel => _playerModel;

        #endregion


        #region IObservable

        public void Notify(PlayerDataType dataType)
        {
            _observers.ForEach(i => i.Catch(dataType, _playerModel));
        }

        public void Subscribe(IObserver observer)
        {
            if (!_observers.Contains(observer))
            {
                _observers.Add(observer);
            }
        }

        public void Unsubscribe(IObserver observer)
        {
            _observers.Remove(observer);
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
