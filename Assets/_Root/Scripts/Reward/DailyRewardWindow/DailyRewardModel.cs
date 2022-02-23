using UnityEngine;
using System;

namespace RaceMobile.Reward
{
    internal class DailyRewardModel
    {
        private const string LastTimeKey = nameof(LastTimeKey);
        private const string ActiveSlotKey = nameof(ActiveSlotKey);

        public int CurrentActiveSlot
        {
            get => PlayerPrefs.GetInt(ActiveSlotKey);
            set => PlayerPrefs.SetInt(ActiveSlotKey, value);
        }
        public DateTime? LastRewardTime //? Позволяет возвратить Null в структуре (nullable)
        {
            get 
            {
                var time = PlayerPrefs.GetString(LastTimeKey);
                if(!string.IsNullOrEmpty(time) && DateTime.TryParse(time, out DateTime result))
                {
                    return result;
                }
                return null;
            }
            set
            {
                if(value != null)
                {
                    PlayerPrefs.SetString(LastTimeKey, value.ToString());
                }
                else
                {
                    PlayerPrefs.DeleteKey(LastTimeKey);
                }
            }
        }
    }
}