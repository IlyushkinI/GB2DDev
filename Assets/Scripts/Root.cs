using Model.Analytic;
using Profile;
using System.Collections.Generic;
using System.Linq;
using Tools.Ads;
using UnityEngine;


public class Root : MonoBehaviour
{
    [SerializeField]
    private List<GlobalEventSO> _eventsUI;

    [SerializeField] 
    private Transform _placeForUi;

    [SerializeField]
    private UnityAdsTools _ads;

    [Space]
    [SerializeField]
    private List<ItemConfig> _items;

    [Space]
    [SerializeField]
    private UpgradeItemConfigDataSource _upgradeSource;

    [Space]
    [SerializeField]
    private List<AbilityItemConfig> _abilityItems;

    private MainController _mainController;
    private IAnalyticTools _analyticsTools;

    private void Awake()
    {
        _analyticsTools = new UnityAnalyticTools();
        var profilePlayer = new ProfilePlayer(15f, _ads, _analyticsTools);
        profilePlayer.CurrentState.Value = GameState.Start;
        new TrailController(_placeForUi);
        _mainController = new MainController(_placeForUi, profilePlayer, _analyticsTools, _ads, _items, _upgradeSource.ItemConfigs.ToList(), _abilityItems.AsReadOnly(), _eventsUI);
    }

    protected void OnDestroy()
    {
        _mainController?.Dispose();
    }
}
