namespace AI
{
    public sealed class MainController : BaseController
    {

        #region CodeLifeCycles

        public MainController(AIUIEventSO eventSO, IAIUIView aiUIView, PlayerView playerView, EnemyView enemyView)
        {
            var aiUIController = new AIUIController(eventSO, aiUIView);
            AddController(aiUIController);

            var playerController = new PlayerController(playerView, eventSO);
            AddController(playerController);

            var enemyController = new EnemyController();
            AddController(enemyController);

            playerController.Subscribe(aiUIController);
        }

        #endregion

    }
}