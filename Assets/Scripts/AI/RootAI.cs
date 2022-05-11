using UnityEngine;


namespace AI
{
    public sealed class RootAI : MonoBehaviour
    {

        #region Fields

        [SerializeField]
        private AIUIEventSO _eventSO;

        [Space]
        [SerializeField]
        private AIUIView _aiUIView;

        [Space]
        [SerializeField]
        private PlayerView _playerView;

        [SerializeField]
        private EnemyView _enemyView;

        #endregion


        #region UnityMethods

        private void Awake()
        {
            _ = new MainController(_eventSO, _aiUIView, _playerView, _enemyView);
        }

        #endregion

    }
}
