using System;
using TMPro;
using UnityEngine;

namespace RaceMobile.Reward
{
    internal class CurrencyWindowView : MonoBehaviour
    {
        [SerializeField] private TMP_Text countSilver;
        [SerializeField] private TMP_Text countGold;

        public void RefreshSilverUI(int count)
        {
            countSilver.text = count.ToString();
        }
        public void RefreshGoldUI(int count)
        {
            countGold.text = count.ToString();
        }
    }
}