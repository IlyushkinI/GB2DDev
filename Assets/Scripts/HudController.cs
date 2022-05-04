using Profile;
using UnityEngine;

public class HudController : BaseController
{
    private readonly ResourcePath _viewPath = new ResourcePath { PathResource = "Prefabs/Hud" };
    private readonly ProfilePlayer _profilePlayer;
    private readonly HudView _view;

    public HudController(
        Transform placeForUi,
        ProfilePlayer profilePlayer)
    {
        _profilePlayer = profilePlayer;
        _view = LoadView(placeForUi);
        _view.ExitButton(ExitToMain);
    }

    private HudView LoadView(Transform placeForUi)
    {
        var objectView = Object.Instantiate(ResourceLoader.LoadPrefab(_viewPath), placeForUi, false);
        AddGameObjects(objectView);

        return objectView.GetComponent<HudView>();
    }

    private void ExitToMain()
    {
        _profilePlayer.CurrentState.Value = GameState.Start;
    }
}
