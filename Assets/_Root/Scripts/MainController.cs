using RaceMobile.Menu;
using System;
using UnityEngine;

namespace RaceMobile.Base
{
    internal class MainController : BaseController
    {
        private readonly Transform placeForUI;
        private readonly ProfilePlayer profilePlayer;
        private BaseController currentState;

        public MainController(ProfilePlayer playerModel, Transform placeForUI)
        {
            this.placeForUI = placeForUI;
            this.profilePlayer = playerModel;
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
                    currentState = new MainMenuController(placeForUI, profilePlayer);
                    AddController(currentState);
                    break;
                case GameState.Game:
                    currentState?.Dispose();
                    currentState = new GameController(profilePlayer);
                    profilePlayer.analiticTools.SendMessage("StartGame");
                    AddController(currentState);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(gameState), gameState, null);
            }
        }

        protected override void OnDispose()
        {
            profilePlayer.GameStatus.UnsubscribeOnChange(OnGameStateChange);
            base.OnDispose();
            
        }
    }
}