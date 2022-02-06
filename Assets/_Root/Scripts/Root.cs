using Game.Base;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Root : MonoBehaviour
{
    private PlayerModel playerModel;
    private MainController mainController;
    void Start()
    {
        playerModel = new PlayerModel();
        mainController = new MainController(playerModel);
    }

    private void OnDestroy()
    {
        playerModel = null;
        mainController.Dispose();
        mainController = null;
    }
}
