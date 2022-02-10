using RaceMobile.Base;
using UnityEngine;
using RaceMobile.AnaliticsTools;

public class Root : MonoBehaviour
{
    [SerializeField]
    private Transform placeForUI;

    private ProfilePlayer playerModel;
    private MainController mainController;
    private IAnaliticTools analiticTools;
    void Start()
    {
        analiticTools = new UnityAnaliticTools();
        playerModel = new ProfilePlayer(5, analiticTools);
        playerModel.GameStatus.Value = GameState.Menu;
        mainController = new MainController(playerModel, placeForUI);
    }

    private void OnDestroy()
    {
        mainController?.Dispose();
    }
}
