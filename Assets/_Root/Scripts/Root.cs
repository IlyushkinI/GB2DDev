using RaceMobile.Base;
using UnityEngine;

public class Root : MonoBehaviour
{
    [SerializeField]
    private Transform placeForUI;

    private ProfilePlayer playerModel;
    private MainController mainController;
    void Start()
    {
        playerModel = new ProfilePlayer();
        playerModel.GameStatus.Value = GameState.Menu;
        mainController = new MainController(playerModel, placeForUI);
    }

    private void OnDestroy()
    {
        mainController?.Dispose();
    }
}
