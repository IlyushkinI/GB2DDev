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