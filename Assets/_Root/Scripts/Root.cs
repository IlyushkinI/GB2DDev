using RaceMobile.Base;
using UnityEngine;
using RaceMobile.AnaliticsTools;
using RaceMobile.Tools.Ads;

public class Root : MonoBehaviour
{
    [SerializeField]
    private Transform placeForUI;

    [SerializeField]
    private UnityAdsTools adsShower;

    private ProfilePlayer playerModel;
    private MainController mainController;
    private IAnaliticTools analiticTools;

    void Start()
    {
        analiticTools = new UnityAnaliticTools();
        playerModel = new ProfilePlayer(1.0f, analiticTools);
        playerModel.GameStatus.Value = GameState.Menu;
        mainController = new MainController(playerModel, placeForUI, adsShower);
    }


    private void Update()
    {
        if(Input.touchCount > 0)
        {
            var touch = Input.GetTouch(0);
            Debug.Log(touch.phase.ToString());
        }

    }

    private void OnDestroy()
    {
        mainController?.Dispose();
    }
}
