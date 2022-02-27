using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RaceMobile.Base;
using RaceMobile.Tools.ResourceManagment;
using RaceMobile.Tools.Reactive;
using System;

namespace RaceMobile.Reward
{
    internal class CurrencyWindowController : BaseController
    {
        private CurrencyModel currencyWindowModel;
        private readonly ProfilePlayer profilePlayer;

        private readonly CurrencyWindowView currencyWindowView;
        private readonly ResourcePath path = new ResourcePath() { PathResource = "Prefabs/Reward/CurrencyWindow" };
      
        public CurrencyWindowController(Transform placeForUI, ProfilePlayer profilePlayer)
        {
            this.profilePlayer = profilePlayer;
            currencyWindowModel = profilePlayer.CurrencyModel;
            currencyWindowView = LoadView(placeForUI);
            profilePlayer.CurrencyModel.GoldUpdate += currencyWindowView.RefreshGoldUI;
            profilePlayer.CurrencyModel.SilverUpdate += currencyWindowView.RefreshSilverUI;

        }

        protected override void OnDispose()
        {
            profilePlayer.CurrencyModel.SilverUpdate -= currencyWindowView.RefreshSilverUI;
            profilePlayer.CurrencyModel.GoldUpdate -= currencyWindowView.RefreshGoldUI;

                        
        }

        public void AddSilver(int count)
        {
            currencyWindowModel.Silver += count;
            currencyWindowView.RefreshSilverUI(currencyWindowModel.Silver);
        }

        public void AddGold(int count)
        {
           currencyWindowModel.Gold += count;
           currencyWindowView.RefreshGoldUI(currencyWindowModel.Gold);
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