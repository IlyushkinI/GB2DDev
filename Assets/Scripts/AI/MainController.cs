namespace AI
{
    public sealed class MainController : BaseController
    {

        #region CodeLifeCycles

        public MainController(AIUIEventSO eventsUI, IAIUIView aiUIView, PlayerView playerView, EnemyView enemyView)
        {
            var aiUIController = new AIUIController(eventsUI, aiUIView);
            AddController(aiUIController);

            var playerController = new PlayerController(playerView, eventsUI);
            AddController(playerController);

            var enemyController = new EnemyController(enemyView);
            AddController(enemyController);

            playerController.Subscribe(aiUIController);
            playerController.Subscribe(enemyController);
            
            enemyController.Subscribe(aiUIController);
        }

        #endregion

    }
}