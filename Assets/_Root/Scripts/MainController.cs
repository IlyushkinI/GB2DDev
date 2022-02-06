using Game.Menu;
using System;

namespace Game.Base
{
    internal class MainController : BaseController
    {
        private readonly PlayerModel playerModel;
        private BaseController currentState;

        public MainController(PlayerModel playerModel)
        {
            this.playerModel = playerModel;
            playerModel.GameStatus.SubscribeOnChange(OnGameStateChange);
        }

        private void OnGameStateChange(GameState gameState)
        {
            switch (gameState)
            {
                case GameState.None:
                    currentState.Dispose();
                    break;
                case GameState.Menu:
                    currentState?.Dispose();
                    currentState = new MainMenuController();
                    AddController(currentState);
                    break;
                case GameState.Game:
                    currentState.Dispose();
                    currentState = new GameController();
                    AddController(currentState);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(gameState), gameState, null);
            }
        }

        protected override void OnDispose()
        {
            base.OnDispose();
            playerModel.GameStatus.UnsubscribeOnChange(OnGameStateChange);
        }
    }
}