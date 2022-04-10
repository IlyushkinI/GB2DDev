using Profile;
using UnityEngine;

public class MainMenuController : BaseController
{
    private readonly ResourcePath _viewPath = new ResourcePath {PathResource = "Prefabs/mainMenu"};
    private readonly ResourcePath _touchingPath = new ResourcePath { PathResource = "Prefabs/Touching" };
    private readonly ProfilePlayer _profilePlayer;
    private readonly MainMenuView _view;
    private readonly MoveByTouch _moveByTouch;

    public MainMenuController(Transform placeForUi, ProfilePlayer profilePlayer)
    {
        _profilePlayer = profilePlayer;
        _view = LoadView(placeForUi);
        _moveByTouch = LoadTouchView();

        _view.Init(StartGame);
    }

    private MainMenuView LoadView(Transform placeForUi)
    {
        var objectView = Object.Instantiate(ResourceLoader.LoadPrefab(_viewPath), placeForUi, false);
        AddGameObjects(objectView);

        return objectView.GetComponent<MainMenuView>();
    }

    private MoveByTouch LoadTouchView()
    {
        var touchingView = Object.Instantiate(ResourceLoader.LoadPrefab(_touchingPath));
        AddGameObjects(touchingView);

        return touchingView.GetComponent<MoveByTouch>();
    }

    private void StartGame()
    {
        _profilePlayer.CurrentState.Value = GameState.Game;
    }
}

