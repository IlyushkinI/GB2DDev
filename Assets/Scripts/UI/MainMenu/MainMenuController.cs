using Model.Analytic;
using Profile;
using System;
using System.Collections.Generic;
using Tools.Ads;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : BaseController
{
    private readonly string _sceneAI = "AI";
    private readonly ResourcePath _viewPath = new ResourcePath { PathResource = "Prefabs/mainMenu" };
    private readonly ProfilePlayer _profilePlayer;
    private readonly IAnalyticTools _analytics;
    private readonly IAdsShower _ads;
    private readonly MainMenuView _view;
    private InputControllerType _inputControllerType;
    private readonly ShedController _shedController;
    private readonly GlobalEventSO _eventsShed;

    public InputControllerType ControllerType => _inputControllerType;

    public MainMenuController(
        Transform placeForUI,
        ProfilePlayer profilePlayer,
        InputControllerType inputControllerType,
        IAnalyticTools analytics,
        IAdsShower ads,
        GlobalEventSO eventsShed,
        List<ItemConfig> itemsConfig,
        IReadOnlyList<UpgradeItemConfig> upgradeItems)
    {
        _inputControllerType = inputControllerType;
        _profilePlayer = profilePlayer;
        _analytics = analytics;
        _ads = ads;
        _view = LoadView(placeForUI);
        _eventsShed = eventsShed;
        _view.Init(StartGame, ChooseInput, EnterShed, StartBattle);

        _eventsShed.GlobalEventAction += EventsShedHandler;

        _shedController = new ShedController(upgradeItems, itemsConfig, _profilePlayer.CurrentCar, placeForUI, _eventsShed);
        AddController(_shedController);
    }

    private MainMenuView LoadView(Transform placeForUi)
    {
        var objectView = GameObject.Instantiate(ResourceLoader.LoadPrefab(_viewPath), placeForUi, false).GetComponent<MainMenuView>();
        AddGameObjects(objectView.gameObject);

        ConfigureInputDropdown(objectView);

        return objectView;
    }

    private void StartGame()
    {
        _analytics.SendMessage("Start", new Dictionary<string, object>());
        _ads.ShowInterstitial();
        _profilePlayer.CurrentState.Value = GameState.Game;
    }

    private void ChooseInput(int inputControllerType)
    {
        _inputControllerType = (InputControllerType)(inputControllerType + 1);
    }

    private void ConfigureInputDropdown(MainMenuView view)
    {
        string[] allInputTypes = Enum.GetNames(typeof(InputControllerType));
        view.DropdownInputSelect.ClearOptions();
        view.DropdownInputSelect.AddOptions(new List<string>(allInputTypes));
        view.DropdownInputSelect.value = (int)_inputControllerType - 1;
    }

    private void EnterShed()
    {
        _view.isActive = false;
        _shedController.Enter();
    }

    private void ExitShed()
    {
        _view.isActive = true;
        _shedController.Exit();
    }

    private void StartBattle()
    {
        SceneManager.LoadScene(_sceneAI);
    }

    private void EventsShedHandler(UIElements caller)
    {
        if (caller == UIElements.ExitShed)
        {
            ExitShed();
        }
    }

    protected override void OnDispose()
    {
        _eventsShed.GlobalEventAction -= EventsShedHandler;
    }

}
