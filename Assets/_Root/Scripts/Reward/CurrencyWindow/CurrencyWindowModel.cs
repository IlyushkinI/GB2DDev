using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RaceMobile.Reward
{
    public class CurrencyWindowModel
    {
        private const string SilverKey = nameof(SilverKey);
        private const string GoldKey = nameof(GoldKey);

        private int silver;
        private int gold;

        public int Silver
        {
            get => PlayerPrefs.GetInt(SilverKey);
            set => PlayerPrefs.SetInt(SilverKey, value);
        }

        public int Gold
        {
            get => PlayerPrefs.GetInt(GoldKey);
            set => PlayerPrefs.SetInt(GoldKey, value);
        }
    }
}