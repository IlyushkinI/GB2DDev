using TMPro;
using UnityEngine;


namespace AI
{
    public sealed class AIUIView : MonoBehaviour, IAIUIView
    {

        #region Fields

        [Space]
        [SerializeField]
        private TextMeshProUGUI _textDataMoney;

        [SerializeField]
        private TextMeshProUGUI _textDataHealth;

        [SerializeField]
        private TextMeshProUGUI _textDataForce;

        [Space]
        [SerializeField]
        private TextMeshProUGUI _textDataEnemy;

        #endregion


        #region Properties

        public int MoneySet { set => _textDataMoney.text = $"Money :\t{value:D3} $"; }
        public int HealthSet { set => _textDataHealth.text = $"Health :\t{value:D3} ♥"; }
        public int ForceSet { set => _textDataForce.text = $"Force :\t{value:D3} †"; }
        public int EnemyPowerSet { set => _textDataEnemy.text = $"Enemy power :\t{value:D3}"; }

        #endregion

    }
}
