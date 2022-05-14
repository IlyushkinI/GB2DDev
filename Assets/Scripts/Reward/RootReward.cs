using UnityEngine;


namespace Reward
{
    public sealed class RootReward : MonoBehaviour
    {

        #region Fields

        [SerializeField]
        private UIEventSO _eventsUI;

        [Space]
        [SerializeField]
        private RewardsView _rewardsView;

        [SerializeField]
        private StorageView _storageView;

        [Space]
        [SerializeField]
        private GameObject _rewardCellGO;
        
        [SerializeField]
        private GameObject _storageCellGO;

        #endregion


        #region UnityMethods

        private void Awake()
        {
            _ = new MainController(_eventsUI);
        }

        #endregion

    }
}