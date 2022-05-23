using Model.Analytic;
using Profile;
using System;
using System.Collections.Generic;
using Tools.Ads;
using UnityEditor;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;


public class MainMenuController : BaseController
{
    private const string SCENE_AI_NAME = "AI";
    private const string SCENE_REWARD_NAME = "Reward";

    private readonly ResourcePath _viewPath = new ResourcePath { PathResource = "Prefabs/MainMenu/mainMenu" };
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
        IReadOnlyList<UpgradeItemConfig> upgradeItems,
        AssetReference sheedPrefab)
    {
        _inputControllerType = inputControllerType;
        _profilePlayer = profilePlayer;
        _analytics = analytics;
        _ads = ads;
        _eventsShed = eventsShed;
        
        _view = LoadView(placeForUI);
        _view.Init(StartGame, ChooseInput, EnterShed, StartBattle, OpenRewards, DoExit);

        _eventsShed.GlobalEventAction += EventsShedHandler;

        _shedController = new ShedController(upgradeItems, itemsConfig, _profilePlayer.CurrentCar, placeForUI, _eventsShed, sheedPrefab);
        AddController(_shedController);
    }

    private MainMenuView LoadView(Transform placeForUi)
    {
        var objectView = GameObject.Instantiate(ResourceLoader.LoadPrefab(_viewPath), placeForUi, false).GetComponent<MainMenuView>();
        AddGameObjects(objectView.gameObject);

        ConfigureInputDropdown(objectView);

        return objectView;
    }

    private void DoExit()
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
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
        SceneManager.LoadScene(SCENE_AI_NAME);
    }

    private void OpenRewards()
    {
        SceneManager.LoadScene(SCENE_REWARD_NAME);
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
