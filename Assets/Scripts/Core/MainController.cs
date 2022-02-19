﻿using CoreGame;
using Data;
using Features.InventoryFeature;
using Features.ShedFeature;
using Model;
using System.Collections.Generic;
using System.Linq;
using Tools.ResourceManagement;
using UnityEngine;

namespace Core
{
    public class MainController : BaseController
    {
        public MainController(Transform placeForUi, ProfilePlayer profilePlayer)
        {
            _profilePlayer = profilePlayer;
            _placeForUi = placeForUi;

            var itemsSource =
                ResourceLoader.LoadDataSource<ItemConfig>(new ResourcePath()
                { PathResource = "Data/ItemsSource" });
            _itemsConfig = itemsSource.Content.ToList();

            var upgradeSource = 
                ResourceLoader.LoadDataSource<UpgradeItemConfig>(new ResourcePath()
                { PathResource = "Data/UpgradesSource" });
            _upgradesConfig = upgradeSource.Content.ToList();

            var abilityItemsSource =
                ResourceLoader.LoadDataSource<AbilityItemConfig>(new ResourcePath()
                { PathResource = "Data/AbilityItemConfig" });
            _abilityItemsConfig = abilityItemsSource.Content.ToList();

            OnChangeGameState(_profilePlayer.CurrentState.Value);
            profilePlayer.CurrentState.SubscribeOnChange(OnChangeGameState);
        }

        private MainMenuController _mainMenuController;
        private ShedController _shedController;
        private GameController _gameController;
        private InventoryController _inventoryController;
        private readonly Transform _placeForUi;
        private readonly ProfilePlayer _profilePlayer;
        private readonly List<ItemConfig> _itemsConfig;
        private readonly List<UpgradeItemConfig> _upgradesConfig;
        private readonly List<AbilityItemConfig> _abilityItemsConfig;

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
                    _shedController = new ShedController(_upgradesConfig, _itemsConfig, _profilePlayer.CurrentCar);
                    _shedController.Enter();
                    _shedController.Exit();
                    _gameController?.Dispose();
                    _inventoryController?.Dispose();
                    break;
                case GameState.Game:
                    var inventoryModel = new InventoryModel();
                    _inventoryController = new InventoryController(_itemsConfig, inventoryModel);
                    _inventoryController.ShowInventory();
                    _gameController = new GameController(_profilePlayer, _abilityItemsConfig, inventoryModel, _placeForUi);
                    _mainMenuController?.Dispose();
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
}