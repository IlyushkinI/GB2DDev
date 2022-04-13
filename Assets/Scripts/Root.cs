using Profile.Analytic;
using Profile;
using UnityEngine;
using Tools;

public class Root : MonoBehaviour
{
    [SerializeField] 
    private Transform _placeForUi;
    [SerializeField]
    private UnityAdsTools _unityAdsTool;
    private MainController _mainController;

    private void Awake()
    {
        var profilePlayer = new ProfilePlayer(15f, new  UnityAnalyticTools());
        profilePlayer.CurrentState.Value = GameState.Start;
        _mainController = new MainController(_placeForUi, profilePlayer, _unityAdsTool);
    }

    protected void OnDestroy()
    {
        _mainController?.Dispose();
    }
}
