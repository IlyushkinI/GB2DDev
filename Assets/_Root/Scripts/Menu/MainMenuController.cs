using UnityEngine;
using RaceMobile.Base;
using RaceMobile.Tools.ResourceManagment;
using RaceMobile;

namespace RaceMobile.Menu
{
    internal class MainMenuController  : BaseController
    {
        private readonly ProfilePlayer playerModel;
        private readonly MainMenuView view;
        private readonly ResourcePath resourcePath = new ResourcePath() { PathResource = "Prefabs/MainMenu"};

        public MainMenuController(Transform placeForUI, ProfilePlayer playerModel)
        {
            this.playerModel = playerModel;
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
            playerModel.GameStatus.Value = GameState.Game;
        }

    }

}