using System;
using System.Collections.Generic;
using UnityEngine;


namespace Reward
{
    public sealed class StorageModel : IStorageModel, IObservable
    {

        #region Fields

        private const string CURRENT_ITEM = "CurrentItem";
        private Dictionary<Currency, int> _storage;
        private List<IObserver> _observers;

        #endregion


        #region CodeLifeCycles

        public StorageModel()
        {
            var currencies = Enum.GetValues(typeof(Currency));
            _storage = new Dictionary<Currency, int>(currencies.Length);

            foreach (Currency currency in currencies)
            {
                _storage.Add(currency, PlayerPrefs.GetInt(currency.ToString(), 0));
            }

            _observers = new List<IObserver>();
        }

        #endregion


            #region IStorageModel

        public int CurrentRewardItem { get => PlayerPrefs.GetInt(CURRENT_ITEM, 0); set => PlayerPrefs.SetInt(CURRENT_ITEM, value); }

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