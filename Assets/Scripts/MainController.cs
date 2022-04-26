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
    private readonly Transform _placeForUi;
    private readonly ProfilePlayer _profilePlayer;
    private readonly IAnalyticTools _analyticsTools;
    private readonly IAdsShower _ads;
    private Dictionary<string, object> _analyticDataInputTypeSelected;

    public MainController(Transform placeForUi, ProfilePlayer profilePlayer, IAnalyticTools analyticsTools, IAdsShower ads)
    {
        _profilePlayer = profilePlayer;
        _analyticsTools = analyticsTools;
        _ads = ads;
        _placeForUi = placeForUi;
        _analyticDataInputTypeSelected = new Dictionary<string, object>();
        
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
                _mainMenuController = new MainMenuController(_placeForUi, _profilePlayer, _inputType, _analyticsTools, _ads);
                _analyticDataInputTypeSelected.Add(_inputType.ToString(), null);
                _analyticsTools.SendMessage("InputTypeSelected", _analyticDataInputTypeSelected);
                _gameController?.Dispose();
                break;
            case GameState.Game:
                _inputType = _mainMenuController.ControllerType;
                _gameController = new GameController(_profilePlayer, _inputType);
                _mainMenuController?.Dispose();
                break;
            default:
                _mainMenuController?.Dispose();
                _gameController?.Dispose();
                break;
        }
    }
}
