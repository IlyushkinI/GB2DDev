using Model.Analytic;
using Profile;
using Tools.Ads;
using UnityEngine;
using UnityEngine.Purchasing;
using Model.Shop;

public class MainController : BaseController
{
    private MainMenuController _mainMenuController;
    private GameController _gameController;
    private readonly Transform _placeForUi;
    private readonly ProfilePlayer _profilePlayer;
    private readonly IAnalyticTools _analyticsTools;
    private readonly IAdsShower _ads;
    private readonly IShop _shop;

    public MainController(Transform placeForUi, ProfilePlayer profilePlayer, IAnalyticTools analyticsTools, IAdsShower ads, IShop shop)
    {
        _profilePlayer = profilePlayer;
        _analyticsTools = analyticsTools;
        _ads = ads;
        _shop = shop;
        _placeForUi = placeForUi;
        OnChangeGameState(_profilePlayer.CurrentState.Value);
        profilePlayer.CurrentState.SubscribeOnChange(OnChangeGameState);
        
       
    }



    protected override void OnDispose()
    {
        _mainMenuController?.Dispose();
        _gameController?.Dispose();
        _profilePlayer.CurrentState.UnSubscriptionOnChange(OnChangeGameState);
        base.OnDispose();
    }

    private void OnChangeGameState(GameState state)
    {
        switch (state)
        {
            case GameState.Start:
                _mainMenuController = new MainMenuController(_placeForUi, _profilePlayer, _analyticsTools, _ads, _shop);
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
