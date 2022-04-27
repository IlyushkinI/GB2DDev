using System;
using UnityEngine;


public class GameUIController : BaseController
{

    #region Fields

    //private Action action;

    //private event Action BuyAction
    //{
    //    add { action += value; }
    //    remove { action -= value; }
    //}

    private readonly string _buttonExitPath = "Prefabs/ButtonExit";
    private readonly string _buttonStorePath = "Prefabs/ButtonStore";
    private readonly ButtonExitView _buttonExitView;
    private readonly ButtonStoreView _buttonStoreView;

    #endregion


    #region CodeLifeCycles

    public GameUIController(Transform placeForUI)
    {
        //action = new Action(DoBuy);

        _buttonExitView = Resources.Load<ButtonExitView>(_buttonExitPath);
        GameObject.Instantiate(_buttonExitView, placeForUI);
        //AddGameObjects(_buttonExitView.gameObject);

        _buttonStoreView = Resources.Load<ButtonStoreView>(_buttonStorePath);
        //GameObject.Instantiate(_buttonStoreView);
        GameObject.Instantiate(_buttonStoreView, placeForUI);
        //_buttonStoreView = ResourceLoader.LoadPrefab(new ResourcePath { PathResource = _buttonStorePath }).GetComponent<ButtonStoreView>();
        //AddGameObjects(_buttonStoreView.gameObject);
        //_buttonStoreView.SetEvent(action);
        //BuyAction += DoBuy;
    }

    #endregion


    //#region Methods

    //private void DoBuy()
    //{
    //    Debug.LogWarning("buy");
    //}

    //#endregion


    //protected override void OnDispose()
    //{
    //}

}
