using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


public class GameUIController : BaseController
{

    #region Fields

    private readonly string _buttonExitPath = "Prefabs/ButtonExit";
    private readonly string _buttonStorePath = "Prefabs/ButtonStore";
    private readonly ButtonView _buttonExitView;
    private readonly ButtonStoreView _buttonStoreView;
    private readonly List<GlobalEventSO> _eventsUI;

    #endregion


    #region CodeLifeCycles

    public GameUIController(Transform placeForUI, List<GlobalEventSO> eventsUI)
    {
        _eventsUI = eventsUI;

        _buttonExitView = Resources.Load<ButtonView>(_buttonExitPath);
        GameObject.Instantiate(_buttonExitView, placeForUI);
        //AddGameObjects(_buttonExitView.gameObject);

        _buttonStoreView = Resources.Load<ButtonStoreView>(_buttonStorePath);
        GameObject.Instantiate(_buttonStoreView, placeForUI);
        //AddGameObjects(_buttonStoreView.gameObject);

        SubscribeEvents(_eventsUI);
    }

    #endregion


    #region Methods

    private void SubscribeEvents(List<GlobalEventSO> eventsUI)
    {
        foreach (var item in eventsUI)
        {
            item.GlobalEventAction += UIEventHandler;
        }
    }

    private void UnsubscribeEvents(List<GlobalEventSO> eventsUI)
    {
        foreach (var item in eventsUI)
        {
            item.GlobalEventAction += UIEventHandler;
        }
    }

    private void UIEventHandler(UIElements caller)
    {
        switch (caller)
        {
            case UIElements.None:
                break;
            case UIElements.ButtonExit:
                DoExit();
                break;
            case UIElements.ButtonStore:
                PurchaseComplete();
                break;
        }
    }

    private void DoExit()
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    private void PurchaseComplete()
    {
        Debug.Log("store");
        //foreach (var wheel in _wheels)
        //{
        //    wheel.color = Color.green;
        //}
    }

    protected override void OnDispose()
    {
        UnsubscribeEvents(_eventsUI);
    }

    #endregion

}
