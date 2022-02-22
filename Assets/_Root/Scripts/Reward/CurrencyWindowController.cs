using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RaceMobile.Base;
using RaceMobile.Tools.ResourceManagment;
using System;

namespace RaceMobile.Reward
{
    internal class CurrencyWindowController : BaseController
    {

        private readonly CurrencyWindowView currencyWindow;
        private readonly ResourcePath path = new ResourcePath() { PathResource = "Prefabs/Reward/CurrencyWindow" };

        private const string SilverKey = nameof(SilverKey);
        private const string GoldKey = nameof(GoldKey);

        private int Silver
        {
            get => PlayerPrefs.GetInt(SilverKey);
            set => PlayerPrefs.SetInt(SilverKey, value);
        }

        private int Gold
        {
            get => PlayerPrefs.GetInt(GoldKey);
            set => PlayerPrefs.SetInt(GoldKey, value);
        }

        public void AddSilver(int count)
        {
            Silver += count;
            currencyWindow.RefreshSilverUI(Silver);
        }

        public void AddGold(int count)
        {
            Gold += count;
            currencyWindow.RefreshGoldUI(Gold);
        }

        public CurrencyWindowController(Transform placeForUI)
        {
            currencyWindow = LoadView(placeForUI);
        }

        private CurrencyWindowView LoadView(Transform placeForUI)
        {
            var pref = ResourceLoader.LoadPrefab(path);
            var go = GameObject.Instantiate(pref, placeForUI);
            AddGameObject(go);
            return go.GetComponent<CurrencyWindowView>();
        }
    }
}