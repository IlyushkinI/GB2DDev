namespace AI
{
    public sealed class MainController : BaseController
    {

        #region CodeLifeCycles

        public MainController(AIUIEventSO eventsUI, IAIUIView aiUIView, PlayerView playerView, EnemyView enemyView)
        {
            var aiUIController = new AIUIController(aiUIView);
            AddController(aiUIController);

            var playerController = new PlayerController(playerView, eventsUI);
            AddController(playerController);

            var enemyController = new EnemyController(enemyView);
            AddController(enemyController);
            
            var battleController = new BattleController(eventsUI, enemyController.GetEnemyModel, playerController.GetPlayerModel);
            AddController(battleController);

            playerController.Subscribe(aiUIController);
            playerController.Subscribe(enemyController);
            
            enemyController.Subscribe(aiUIController);
        }

        #endregion

    }
}