using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RaceMobile.Tools.Reactive;
using System;

namespace RaceMobile.Reward
{
    public class CurrencyModel
    {
        public event Action<int> SilverUpdate;
        public event Action<int> GoldUpdate;

        private const string SilverKey = nameof(SilverKey);
        private const string GoldKey = nameof(GoldKey);

        public int Silver
        {
            get => PlayerPrefs.GetInt(SilverKey);
            set
            {
                PlayerPrefs.SetInt(SilverKey, value);
                OnGoldUpdate(value);
            }
            
        }

        public int Gold
        {
            get => PlayerPrefs.GetInt(GoldKey);
            set
            {
                PlayerPrefs.SetInt(GoldKey, value);
                OnGoldUpdate(value);
            }
        }

        private void OnSilverUpdate(int count)
        {
            SilverUpdate?.Invoke(count);
        }

        private void OnGoldUpdate(int count)
        {
            GoldUpdate?.Invoke(count);
        }


    }
}