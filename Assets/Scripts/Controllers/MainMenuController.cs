using System;
using System.Collections.Generic;
using Profile;
using UnityEngine;
using UnityEngine.Advertisements;

public class MainMenuController : BaseController
{
    private readonly ResourcePath _viewPath = new ResourcePath { PathResource = "Prefabs/mainMenu" };
    private readonly ProfilePlayer _profilePlayer;
    private readonly MainMenuView _view;
    private ShedController _shedController;



    public MainMenuController(Transform placeForUi, ProfilePlayer profilePlayer, List<ItemConfig> itemsConfig,
        IReadOnlyList<UpgradeItemConfig> upgradeItems)
    {
        _profilePlayer = profilePlayer;
        _view = LoadView(placeForUi);
       
        _view.Init(StartGame);
        _shedController = new ShedController(upgradeItems, itemsConfig, _profilePlayer.CurrentCar, placeForUi, _view, profilePlayer);

    }

    private MainMenuView LoadView(Transform placeForUi)
    {
        var objectView = UnityEngine.Object.Instantiate(ResourceLoader.LoadPrefab(_viewPath), placeForUi, false);
        AddGameObjects(objectView);

        return objectView.GetComponent<MainMenuView>();
    }

    protected override void OnDispose()
    {
        base.OnDispose();
        _shedController?.Dispose();
    }

    private void StartGame()
    {
        _shedController.Enter();
        _shedController.Exit();
        _profilePlayer.CurrentState.Value = GameState.Game;

        _profilePlayer.AnalyticTools.SendMessage("start_game",
            new Dictionary<string, object>() { {"time", Time.realtimeSinceStartup }
    });
    }
}

