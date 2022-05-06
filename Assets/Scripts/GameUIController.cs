using UnityEditor;
using UnityEngine;


public class GameUIController : BaseController
{

    #region Fields

    private readonly string _prefabUI = "Prefabs/UI";
    private readonly GlobalEventSO _eventUI;
    private readonly CarController _carController;

    #endregion


    #region CodeLifeCycles

    public GameUIController(Transform placeForUI, GlobalEventSO eventUI, CarController carController)
    {
        _eventUI = eventUI;
        _carController = carController;

        GameObject.Instantiate(Resources.Load(_prefabUI), placeForUI);

        _eventUI.GlobalEventAction += UIEventHandler;
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

    protected override void OnDispose()
    {
        _eventUI.GlobalEventAction -= UIEventHandler;
    }

    #endregion

}
