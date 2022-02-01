using System;
using System.Collections.Generic;
using Profile;
using Profile.Helpers;
using UnityEngine;
using Object = UnityEngine.Object;

public class MainMenuController : BaseController
{
    private readonly ResourcePath _viewPath = new ResourcePath {PathResource = PathManager.MAIN_MENU};
    private readonly ProfilePlayer _profilePlayer;
    private readonly MainMenuView _view;
    private Dictionary<int, GameObject> _trails = new Dictionary<int, GameObject>();

    public MainMenuController(Transform placeForUi, ProfilePlayer profilePlayer)
    {
        _profilePlayer = profilePlayer;
        _view = LoadView(placeForUi);
        _view.Init(StartGame);
        _view.UpdateTouch += OnUpdateTouch;
    }

    private MainMenuView LoadView(Transform placeForUi)
    {
        var objectView = Object.Instantiate(ResourceLoader.LoadPrefab(_viewPath), placeForUi, false);
        AddGameObjects(objectView);
        
        return objectView.GetComponent<MainMenuView>();
    }

    private void OnUpdateTouch(TouchData data)
    {
        switch (data.Phase)
        {
            case TouchPhase.Began:
                AddNewTrail(data);
                break;
            case TouchPhase.Moved:
                if (_trails.TryGetValue(data.ID, out var trail))
                {
                    trail.transform.position = data.Position;
                }
                break;
            case TouchPhase.Stationary:
                break;
            case TouchPhase.Ended:
            case TouchPhase.Canceled:
                RemoveTrail(data);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
    
    private void AddNewTrail(TouchData data)
    {
        var trail = _view.CreateTrail(data.Position);
        _trails.Add(data.ID,trail);
    }

    private void RemoveTrail(TouchData data)
    {
        _trails.TryGetValue(data.ID, out var trail);
        GameObject.Destroy(trail);
        _trails.Remove(data.ID);
    }

    private void StartGame()
    {
        _profilePlayer.CurrentState.Value = GameState.Game;
    }

    protected override void OnDispose()
    {
        base.OnDispose();
        _view.UpdateTouch -= OnUpdateTouch;
    }
}

