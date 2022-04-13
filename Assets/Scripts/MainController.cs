using Profile;
using Tools;
using UnityEngine;

public class MainController : BaseController
{
    private MainMenuController _mainMenuController;
    private GameController _gameController;
    private readonly Transform _placeForUi;
    private readonly ProfilePlayer _profilePlayer;
    private readonly IAdsShower _adsShower;
    public MainController(Transform placeForUi, ProfilePlayer profilePlayer,IAdsShower adsShower)
    {
        _profilePlayer = profilePlayer;
        _adsShower = adsShower;
        _placeForUi = placeForUi;
        OnChangeGameState(_profilePlayer.CurrentState.Value);
        profilePlayer.CurrentState.SubscribeOnChange(OnChangeGameState);
    }


    protected override void OnChildDispose()
    {
        _mainMenuController?.Dispose();
        _gameController?.Dispose();
        _profilePlayer.CurrentState.UnSubscriptionOnChange(OnChangeGameState);
        base.OnChildDispose();
    }

    private void OnChangeGameState(GameState state)
    {
        switch (state)
        {
            case GameState.Start:
                _mainMenuController = new MainMenuController(_placeForUi, _profilePlayer, _adsShower);
                _gameController?.Dispose();
                break;
            case GameState.Game:
                _gameController = new GameController(_profilePlayer);
                _mainMenuController?.Dispose();
                break;
            default:
                _mainMenuController?.Dispose();
                _gameController?.Dispose();
                break;
        }
    }
}
