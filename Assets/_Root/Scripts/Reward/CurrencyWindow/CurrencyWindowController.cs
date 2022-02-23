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
        private CurrencyWindowModel CurrencyWindowModel;

        private readonly CurrencyWindowView currencyWindowView;
        private readonly ResourcePath path = new ResourcePath() { PathResource = "Prefabs/Reward/CurrencyWindow" };
      

        public void AddSilver(int count)
        {
            CurrencyWindowModel.Silver += count;
            currencyWindowView.RefreshSilverUI(CurrencyWindowModel.Silver);
        }

        public void AddGold(int count)
        {
            CurrencyWindowModel.Gold += count;
            currencyWindowView.RefreshGoldUI(CurrencyWindowModel.Gold);
        }

        public CurrencyWindowController(Transform placeForUI)
        {
            currencyWindowView = LoadView(placeForUI);
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