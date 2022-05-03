using Model.Analytic;
using Profile;
using System;
using System.Collections.Generic;
using Tools.Ads;
using UnityEngine;


public class MainMenuController : BaseController
{
    private readonly ResourcePath _viewPath = new ResourcePath { PathResource = "Prefabs/mainMenu" };
    private readonly ProfilePlayer _profilePlayer;
    private readonly IAnalyticTools _analytics;
    private readonly IAdsShower _ads;
    private readonly MainMenuView _view;
    private InputControllerType _inputControllerType;
    private readonly ShedController _shedController;

    public InputControllerType ControllerType => _inputControllerType;

    public MainMenuController(
        Transform placeForUi,
        ProfilePlayer profilePlayer,
        InputControllerType inputControllerType,
        IAnalyticTools analytics,
        IAdsShower ads,
        ShedController shedController)
    {
        _inputControllerType = inputControllerType;
        _profilePlayer = profilePlayer;
        _analytics = analytics;
        _ads = ads;
        _view = LoadView(placeForUi);
        _shedController = shedController;
        _view.Init(StartGame, ChooseInput, EnterShed);
    }

    private MainMenuView LoadView(Transform placeForUi)
    {
        var objectView = GameObject.Instantiate(ResourceLoader.LoadPrefab(_viewPath), placeForUi, false).GetComponent<MainMenuView>();
        AddGameObjects(objectView.gameObject);

        ConfigureDropdown(objectView);

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

    private void ConfigureDropdown(MainMenuView view)
    {
        string[] allInputTypes = Enum.GetNames(typeof(InputControllerType));
        view.DropdownInputSelect.ClearOptions();
        view.DropdownInputSelect.AddOptions(new List<string>(allInputTypes));
        view.DropdownInputSelect.value = (int)_inputControllerType - 1;
    }

    private void EnterShed()
    {
        _view.isActive = false;
        //_shedController.
        _shedController.Enter();
    }

    private void ExitShed()
    {
        _view.isActive = true;
    }
}
