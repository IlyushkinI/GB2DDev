using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


public class GameUIController : BaseController
{

    #region Fields

    private readonly string _prefabUI = "Prefabs/Game/UI";
    private readonly GlobalEventSO _eventUI;
    private readonly GlobalEventSO _eventsShed;
    private readonly CarController _carController;
    private readonly ShedController _shedController;

    #endregion


    #region CodeLifeCycles

    public GameUIController(
        Transform placeForUI, 
        GlobalEventSO eventUI, 
        CarController carController,
        GlobalEventSO eventsShed,
        List<ItemConfig> itemsConfig,
        IReadOnlyList<UpgradeItemConfig> upgradeItems,
        ProfilePlayer profilePlayer)
    {
        _eventUI = eventUI;
        _eventsShed = eventsShed;
        _carController = carController;

        GameObject.Instantiate(Resources.Load(_prefabUI), placeForUI);

        _shedController = new ShedController(upgradeItems, itemsConfig, profilePlayer.CurrentCar, placeForUI, eventsShed);
        AddController(_shedController);

        _eventUI.GlobalEventAction += UIEventHandler;
        _eventsShed.GlobalEventAction += EventsShedHandler;
    }

    #endregion


    #region Methods

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
            case UIElements.EnterShed:
                EnterShed();
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
        _carController.CarView.BackWheel.color = Color.green;
        _carController.CarView.FrontWheel.color = Color.green;
    }

    private void EnterShed()
    {
        //_view.isActive = false;
        _shedController.Enter();
    }

    private void ExitShed()
    {
        //_view.isActive = true;
        _shedController.Exit();
    }

    private void EventsShedHandler(UIElements caller)
    {
        if (caller == UIElements.ExitShed)
        {
            ExitShed();
        }
    }

    protected override void OnDispose()
    {
        _eventUI.GlobalEventAction -= UIEventHandler;
        _eventsShed.GlobalEventAction -= EventsShedHandler;
    }

    #endregion

}
