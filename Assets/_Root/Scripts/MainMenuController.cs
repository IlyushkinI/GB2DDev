using UnityEngine;
using Game.Base;
using Game.Tools.ResourceManagment;
using Game;

namespace Game.Menu
{
    internal class MainMenuController  : BaseController
    {
        private readonly PlayerModel playerModel;
        private readonly MainMenuView view;
        private readonly ResourcePath resourcePath = new ResourcePath() { PathResource = "Prefabs/mainMenu"};

        public MainMenuController(Transform placeForUI, PlayerModel playerModel)
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