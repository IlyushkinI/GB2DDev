using Game.Menu;
using System;
using UnityEngine;

namespace Game.Base
{
    internal class MainController : BaseController
    {
        private readonly Transform placeForUI;
        private readonly PlayerModel playerModel;
        private BaseController currentState;

        public MainController(PlayerModel playerModel, Transform placeForUI)
        {
            this.placeForUI = placeForUI;
            this.playerModel = playerModel;
            playerModel.GameStatus.SubscribeOnChange(OnGameStateChange);
            OnGameStateChange(playerModel.GameStatus.Value);
        }

        private void OnGameStateChange(GameState gameState)
        {
            switch (gameState)
            {
                case GameState.None:
                    currentState?.Dispose();
                    break;
                case GameState.Menu:
                    currentState?.Dispose();
                    currentState = new MainMenuController(placeForUI, playerModel);
                    AddController(currentState);
                    break;
                case GameState.Game:
                    currentState?.Dispose();
                    currentState = new GameController();
                    AddController(currentState);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(gameState), gameState, null);
            }
        }

        protected override void OnDispose()
        {
            playerModel.GameStatus.UnsubscribeOnChange(OnGameStateChange);
            base.OnDispose();
            
        }
    }
}