using System;
using System.Collections.Generic;
using UnityEngine;


namespace Reward
{
    public sealed class StorageModel : IStorageModel, IObservable
    {

        #region Fields

        private const string CURRENT_ITEM = "CurrentItem";
        private const string ITEM_DT = "ItemDT";
        private readonly Array _currencies;
        private Dictionary<Currency, int> _storage;
        private List<IObserver> _observers;

        #endregion


        #region CodeLifeCycles

        public StorageModel()
        {
            _currencies = Enum.GetValues(typeof(Currency));
            _storage = new Dictionary<Currency, int>(_currencies.Length);

            foreach (Currency currency in _currencies)
            {
                _storage.Add(currency, PlayerPrefs.GetInt(currency.ToString(), 0));
            }

            _observers = new List<IObserver>();

        }

        #endregion


        #region IStorageModel

        public DateTime WhenCollectingAvailable
        {
            get
            {
                DateTime result = new DateTime();
                DateTime.TryParse(PlayerPrefs.GetString(ITEM_DT), out result);
                return result;
            }
            set => PlayerPrefs.SetString(ITEM_DT, value.ToString());
        }

        public int CurrentRewardItemID { get => PlayerPrefs.GetInt(CURRENT_ITEM, 0); set => PlayerPrefs.SetInt(CURRENT_ITEM, value); }

        public void SetCurrency(Currency currency, int value)
        {
            _storage[currency] = value;
            PlayerPrefs.SetInt(currency.ToString(), value);
            Notify(currency, value);
        }

        public int GetCurrency(Currency currency)
        {
            return _storage[currency];
        }

        public void SetDefaults()
        {
            foreach (Currency currency in _currencies)
            {
                SetCurrency(currency, 0);
            }
            WhenCollectingAvailable = new DateTime();
            CurrentRewardItemID = -1;
        }

        #endregion


        #region IObservable

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

        public void Notify(Currency currency, int value)
        {
            _observers.ForEach(i => i.Catch(currency, value));
        }

        #endregion

    }
}