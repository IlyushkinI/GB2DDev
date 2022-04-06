using DG.Tweening;
using UnityEngine;
using RaceMobile.Base;
using RaceMobile.Tools.ResourceManagment;


namespace RaceMobile.Reward
{
    internal class CurrencyWindowController : BaseController
    {
        private CurrencyModel currencyWindowModel;
        private readonly ProfilePlayer profilePlayer;

        private readonly CurrencyWindowView currencyWindowView;
        private readonly ResourcePath path = new ResourcePath() { PathResource = "Prefabs/Reward/CurrencyWindow" };

        private RectTransform rectTransform;
      
        public CurrencyWindowController(Transform placeForUI, ProfilePlayer profilePlayer)
        {
            this.profilePlayer = profilePlayer;
            currencyWindowModel = profilePlayer.CurrencyModel;
            currencyWindowView = LoadView(placeForUI);
            profilePlayer.CurrencyModel.GoldUpdate += currencyWindowView.RefreshGoldUI;
            profilePlayer.CurrencyModel.SilverUpdate += currencyWindowView.RefreshSilverUI;

            currencyWindowView.RefreshGoldUI(profilePlayer.CurrencyModel.Gold);
            currencyWindowView.RefreshSilverUI(profilePlayer.CurrencyModel.Silver);

            DoTweening();

        }

        private void DoTweening()
        {
            (currencyWindowView.transform as RectTransform).DOShakeRotation(2, 5 * Vector3.forward);
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