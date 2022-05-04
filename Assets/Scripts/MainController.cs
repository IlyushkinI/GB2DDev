using Profile;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MainController : BaseController
{
    public MainController(Transform placeForUi, ProfilePlayer profilePlayer,
        IReadOnlyList<UpgradeItemConfig> upgradeItems,
        IReadOnlyList<AbilityItemConfig> abilityItems)
    {
        _profilePlayer = profilePlayer;
        _placeForUi = placeForUi;
        
        _upgradeItems = upgradeItems;
        _abilityItems = abilityItems;

        var itemsSource =
            ResourceLoader.LoadDataSource<ItemConfig>(new ResourcePath()
                { PathResource = "Data/ItemsSource" });
        _itemsConfig = itemsSource.Content.ToList();

        OnChangeGameState(_profilePlayer.CurrentState.Value);
        profilePlayer.CurrentState.SubscribeOnChange(OnChangeGameState);
    }

    private MainMenuController _mainMenuController;
    private ShedController _shedController;
    private GameController _gameController;
    private HudController _hud;
    private InventoryController _inventoryController;
    private Transform _placeForUi;
    private ProfilePlayer _profilePlayer;
    private List<ItemConfig> _itemsConfig;
    private IReadOnlyList<UpgradeItemConfig> _upgradeItems;
    private IReadOnlyList<AbilityItemConfig> _abilityItems;

    protected override void OnDispose()
    {
        AllClear();

        _profilePlayer.CurrentState.UnSubscriptionOnChange(OnChangeGameState);
        base.OnDispose();
    }

    private void OnChangeGameState(GameState state)
    {
        switch (state)
        {
            case GameState.Start:
                _mainMenuController = new MainMenuController(_placeForUi, _profilePlayer);
                _inventoryController?.Dispose();
                _hud?.Dispose();
                _shedController?.Dispose();
                _gameController?.Dispose();
                break;
            case GameState.Shed:
                _shedController = new ShedController(_placeForUi, _upgradeItems, _itemsConfig, _profilePlayer);
                _shedController.Enter();
                _mainMenuController?.Dispose();
                break;
            case GameState.Game:
                var inventoryModel = new InventoryModel();
                _inventoryController = new InventoryController(_itemsConfig, inventoryModel);
                _inventoryController.ShowInventory();
                _gameController = new GameController(_profilePlayer, _abilityItems, inventoryModel);
                _hud = new HudController(_placeForUi, _profilePlayer);
                _mainMenuController?.Dispose();
                _shedController?.Dispose();
                break;
            default:
                AllClear();
                break;
        }
    }

    private void AllClear()
    {
        _inventoryController?.Dispose();
        _mainMenuController?.Dispose();
        _gameController?.Dispose();
    }
}
