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
            
            var battleController = new BattleController(eventsUI, enemyController.GetEnemyModel, aiUIController);
            AddController(battleController);

            var levelController = new LevelController(eventsUI, this);
            AddController(levelController);

            playerController.Subscribe(aiUIController);
            playerController.Subscribe(enemyController);
            playerController.Subscribe(battleController);
            
            enemyController.Subscribe(aiUIController);
        }

        #endregion

    }
}