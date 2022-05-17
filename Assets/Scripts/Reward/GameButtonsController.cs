using UnityEngine.SceneManagement;


namespace Reward
{
    public sealed class GameButtonsController : BaseController
    {

        #region Fields

        private readonly string _sceneMainMenu = "Game";
        private UIEventSO _eventsUI;

        #endregion


        #region CodeLifeCycles

        public GameButtonsController(UIEventSO eventsUI)
        {
            _eventsUI = eventsUI;
            _eventsUI.UIEvent += UIEventHadler;
        }

        protected override void OnDispose()
        {
            _eventsUI.UIEvent -= UIEventHadler;
        }

        #endregion


        #region Methods

        private void UIEventHadler(UIElement caller)
        {
            switch (caller)
            {
                case UIElement.ButtonExit:
                    SceneManager.LoadScene(_sceneMainMenu);
                    break;
                default:
                    break;
            }
        }

        #endregion

    }
}