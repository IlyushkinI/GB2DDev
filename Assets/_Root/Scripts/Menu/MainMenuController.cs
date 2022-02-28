using UnityEngine;
using RaceMobile.Base;
using RaceMobile.Tools.ResourceManagment;
using RaceMobile.Tools.Ads;

namespace RaceMobile.Menu
{
    internal class MainMenuController  : BaseController
    {
        private readonly ProfilePlayer profilePlayer;
        private readonly MainMenuView view;
        private readonly ResourcePath resourcePath = new ResourcePath() { PathResource = "Prefabs/MainMenu"};
        private readonly IAdsShower adsShower;

        public MainMenuController(Transform placeForUI, ProfilePlayer playerModel, IAdsShower ads)
        {
            adsShower = ads;
            this.profilePlayer = playerModel;
            view = LoadView(placeForUI);
            view.Init(StartGame);

        }

        private MainMenuView LoadView(Transform placeForUI)
        {
            var pref = ResourceLoader.LoadPrefab(resourcePath);
            GameObject go =  Object.Instantiate(pref, placeForUI, false);
            AddGameObject(go);

            return go.GetComponent<MainMenuView>();
        }

        private void StartGame()
        {

            profilePlayer.GameStatus.Value = GameState.Game;
            adsShower.ShowInterstitial();

        }

    }

}