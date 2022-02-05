using Model.Analytic;
using Profile;
using Tools.Ads;
using UnityEngine;
using Model.Shop;
using System.Collections.Generic;

public class Root : MonoBehaviour
{
    [SerializeField] 
    private Transform _placeForUi;

    [SerializeField] private UnityAdsTools _ads;
    [SerializeField] private List<ShopProduct> _shopProducts = new List<ShopProduct>();

    private MainController _mainController;
    private IAnalyticTools _analyticsTools;
    private IShop _shop;

    private void Awake()
    {
        _shop = new ShopTools(_shopProducts);
        var profilePlayer = new ProfilePlayer(15f);
        _analyticsTools = new UnityAnalyticTools();
        profilePlayer.CurrentState.Value = GameState.Start;
        _mainController = new MainController(_placeForUi, profilePlayer, _analyticsTools, _ads, _shop);
    }

    protected void OnDestroy()
    {
        _mainController?.Dispose();
    }
}
