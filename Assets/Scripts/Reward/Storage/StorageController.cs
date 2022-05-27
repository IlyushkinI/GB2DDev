using System;


namespace Reward
{
    public class StorageController : BaseController, IObserver, IStorageController
    {

        #region Fields

        private UIController _uiController;
        private StorageModel _storage;

        #endregion


        #region CodeLifeCycles

        public StorageController(UIController uiController)
        {
            _uiController = uiController;

            _storage = new StorageModel();

            foreach (Currency currency in Enum.GetValues(typeof(Currency)))
            {
                _uiController.SetStorageItemValue(currency, _storage.GetCurrency(currency));
            }

            _storage.Subscribe(this);
        }

        protected override void OnDispose()
        {
            _storage.Unsubscribe(this);
        }

        #endregion


        #region IStorageController

        public IStorageModel StorageModel => _storage;

        #endregion


        #region IObserver

        public void Catch(Currency currency, int value)
        {
            _uiController.SetStorageItemValue(currency, value);
        }

        #endregion

    }
}