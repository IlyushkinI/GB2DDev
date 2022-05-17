using System.Collections.Generic;
using UnityEngine;


namespace Reward
{
    [CreateAssetMenu(menuName = "Reward/Config", fileName = "RewardsConfig")]
    public sealed class RewardsConfSO : ScriptableObject
    {

        #region Fields

        [SerializeField]
        private List<RewardData> _rewards;

        #endregion


        #region Properties

        public List<RewardData> Rewards => _rewards;

        #endregion

    }
}
