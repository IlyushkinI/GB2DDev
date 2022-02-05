using System.Collections.Generic;
using Model.Analytic;
using Profile;
using Tools.Ads;
using UnityEngine;
using Model.Shop;
using UnityEngine.Purchasing;


public class MainMenuController : BaseController
{
    private readonly ResourcePath _viewPath = new ResourcePath {PathResource = "Prefabs/mainMenu"};
    private readonly ProfilePlayer _profilePlayer;
    private readonly IAnalyticTools _analytics;
    private readonly IAdsShower _ads;
    private readonly IShop _shop;
    private readonly MainMenuView _view;

    public MainMenuController(Transform placeForUi, ProfilePlayer profilePlayer, IAnalyticTools analytics, IAdsShower ads, IShop shop)
    {
        _profilePlayer = profilePlayer;
        _analytics = analytics;
        _ads = ads;
        _shop = shop;
        _view = LoadView(placeForUi);
        _view.Init(StartGame, Buy);
        _view.GetComponent<SliceInMainMenuView>().Initialize();
        

    }

    private MainMenuView LoadView(Transform placeForUi)
    {
        var objectView = Object.Instantiate(ResourceLoader<GameObject>.LoadPrefab(_viewPath), placeForUi, false);
        AddGameObjects(objectView);
        
        return objectView.GetComponent<MainMenuView>();
    }

    private void StartGame()
    {
        _analytics.SendMessage("Start", new Dictionary<string, object>());
        _ads.ShowInterstitial();
        _profilePlayer.CurrentState.Value = GameState.Game;
    }

    private void Buy()
    {
        _shop.Buy("Money");
    }

}

