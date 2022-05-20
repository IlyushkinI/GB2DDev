using Model.Analytic;
using Profile;
using System.Collections.Generic;
using Tools.Ads;
using UnityEngine;


public class MainController : BaseController
{

    private InputControllerType _inputType = InputControllerType.Buttons;
    private MainMenuController _mainMenuController;
    private GameController _gameController;
    private readonly Transform _placeForUI;
    private readonly ProfilePlayer _profilePlayer;
    private readonly List<ItemConfig> _itemsConfig;
    private readonly IAnalyticTools _analyticsTools;
    private readonly IAdsShower _ads;
    private Dictionary<string, object> _analyticDataInputTypeSelected;
    private readonly IReadOnlyList<UpgradeItemConfig> _upgradeItems;
    private readonly IReadOnlyList<AbilityItemConfig> _abilityItems;
    private readonly GlobalEventSO _eventsGameUI;
    private readonly GlobalEventSO _eventsShed;

    public MainController(
        Transform placeForUI,
        ProfilePlayer profilePlayer,
        IAnalyticTools analyticsTools,
        IAdsShower ads,
        List<ItemConfig> itemsConfig,
        IReadOnlyList<UpgradeItemConfig> upgradeItems,
        IReadOnlyList<AbilityItemConfig> abilityItems,
        GlobalEventSO eventsGameUI,
        GlobalEventSO eventsShed)
    {
        _profilePlayer = profilePlayer;
        _analyticsTools = analyticsTools;
        _ads = ads;
        _placeForUI = placeForUI;
        _itemsConfig = itemsConfig;
        _upgradeItems = upgradeItems;
        _abilityItems = abilityItems;
        _analyticDataInputTypeSelected = new Dictionary<string, object>();
        _eventsGameUI = eventsGameUI;
        _eventsShed = eventsShed;

        OnChangeGameState(_profilePlayer.CurrentState.Value);
        profilePlayer.CurrentState.SubscribeOnChange(OnChangeGameState);
    }

    protected override void OnDispose()
    {
        AllClear();

        _profilePlayer.CurrentState.UnSubscriptionOnChange(OnChangeGameState);
        base.OnDispose();
    }

    private void OnChangeGameState(GameState state)
    {
        switch (state)
        {
            case GameState.Start:
                _mainMenuController = new MainMenuController(_placeForUI, _profilePlayer, _inputType, _analyticsTools, _ads, _eventsShed, _itemsConfig, _upgradeItems);
                _gameController?.Dispose();
                break;
            case GameState.Game:
                _inputType = _mainMenuController.ControllerType;
                _analyticDataInputTypeSelected.Add(_inputType.ToString(), null);
                _analyticsTools.SendMessage("InputTypeSelected", _analyticDataInputTypeSelected);
                _gameController = new GameController(_profilePlayer, _inputType, _placeForUI, _abilityItems, _eventsGameUI, _eventsShed, _itemsConfig, _upgradeItems);
                _mainMenuController?.Dispose();
                break;
            default:
                AllClear();
                break;
        }
    }

    private void AllClear()
    {
        _mainMenuController?.Dispose();
        _gameController?.Dispose();
    }

}
