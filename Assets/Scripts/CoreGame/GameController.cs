﻿using System.Collections.Generic;
using Core;
using Data;
using Features.AbilitiesFeature;
using Features.InventoryFeature;
using InputFeature;
using Model;
using Tools.ResourceManagement;
using Tools.RX;
using UnityEngine;

namespace CoreGame
{
    public class GameController : BaseController
    {
        public GameController(ProfilePlayer profilePlayer, IReadOnlyList<AbilityItemConfig> configs, InventoryModel inventoryModel, Transform uiRoot)
        {
            var leftMoveDiff = new SubscriptionProperty<float>();
            var rightMoveDiff = new SubscriptionProperty<float>();

            var tapeBackgroundController = new TapeBackgroundController(leftMoveDiff, rightMoveDiff);
            AddController(tapeBackgroundController);

            var inputGameController = new InputGameController(leftMoveDiff, rightMoveDiff, profilePlayer.CurrentCar);
            AddController(inputGameController);

            var carController = new CarController();
            AddController(carController);

            var abilityRepository = new AbilityRepository(configs);
            var abilityView =
                ResourceLoader.LoadAndInstantiateView<AbilitiesView>(
                    new ResourcePath() { PathResource = "Prefabs/AbilitiesView" }, uiRoot);
            var abilitiesController = new AbilitiesController(carController, inventoryModel, abilityRepository,
                abilityView);
            AddController(abilitiesController);

        }
    }
}