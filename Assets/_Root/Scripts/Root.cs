using RaceMobile.Base;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Root : MonoBehaviour
{
    [SerializeField]
    private Transform placeForUI;

    private PlayerModel playerModel;
    private MainController mainController;
    void Start()
    {
        playerModel = new PlayerModel();
        playerModel.GameStatus.Value = GameState.Menu;
        mainController = new MainController(playerModel, placeForUI);
    }

    private void OnDestroy()
    {
        mainController?.Dispose();
    }
}
