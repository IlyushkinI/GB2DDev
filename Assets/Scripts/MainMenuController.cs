using CarInput;
using Profile;
using Tools;
using UnityEngine;

public class MainMenuController : BaseController
{
    private readonly ResourcePath _viewPath = new ResourcePath { PathResource = "Prefabs/mainMenu" };
    private readonly ResourcePath _tileRenderPath = new ResourcePath { PathResource = "Prefabs/tileRender" };

    private readonly ProfilePlayer _profilePlayer;
    private readonly IAdsShower _adsShower;
    private readonly MainMenuView _view;
    private readonly TileRenderView _tileView;

    public MainMenuController(Transform placeForUi, ProfilePlayer profilePlayer, IAdsShower adsShower)
    {
        _profilePlayer = profilePlayer;
        _adsShower = adsShower;
        _view = LoadView(placeForUi);
        _tileView = LoadTileRender(placeForUi);
        _view.Init(StartGame);
        _tileView.Init(StartGame);
    }
    private TileRenderView LoadTileRender(Transform placeForUi)
    {
        var objectView = Object.Instantiate(ResourceLoader.LoadPrefab(_tileRenderPath), placeForUi, false);
        AddGameObjects(objectView);
        return objectView.GetComponent<TileRenderView>();
    }
    private MainMenuView LoadView(Transform placeForUi)
    {
        var objectView = Object.Instantiate(ResourceLoader.LoadPrefab(_viewPath), placeForUi, false);
        AddGameObjects(objectView);

        return objectView.GetComponent<MainMenuView>();
    }

    private void StartGame()
    {
        _profilePlayer.CurrentState.Value = GameState.Game;
        _adsShower.ShowInterstitial();
        _profilePlayer.AnalyticTools.SendMessage("start_game");

    }
}

