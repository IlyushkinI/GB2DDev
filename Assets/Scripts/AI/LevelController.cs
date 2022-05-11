using UnityEngine.SceneManagement;


namespace AI
{
    public class LevelController : BaseController
    {

        #region Fields

        private readonly string _sceneAI = "AI";
        private readonly string _sceneMainMenu = "Game";
        private readonly AIUIEventSO _eventsUI;
        
        #endregion


        #region CodeLifeCycles

        public LevelController(AIUIEventSO eventsUI)
        {
            _eventsUI = eventsUI;

            _eventsUI.UIEvent += UIEventHandler;
        }

        #endregion


        #region Methods

        private void UIEventHandler(AIUIElement caller, PlayerDataType _, int __)
        {
            switch (caller)
            {
                case AIUIElement.ButtonExit:
                    SceneManager.LoadScene(_sceneMainMenu);
                    break;
                case AIUIElement.ButtonRestart:
                    SceneManager.LoadScene(_sceneAI);
                    break;
                default:
                    break;
            }
        }

        #endregion


        #region BaseController

        protected override void OnDispose()
        {
            _eventsUI.UIEvent -= UIEventHandler;
        }

        #endregion

    }
}