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


        #region IAIUIView

        public int MoneySet { set => _textDataMoney.text = $"Money :\t{value:D3} {PlayerDataType.Money.ToUnit()}"; }
        public int HealthSet { set => _textDataHealth.text = $"Health :\t{value:D3} {PlayerDataType.Health.ToUnit()}"; }
        public int ForceSet { set => _textDataForce.text = $"Force :\t{value:D3} {PlayerDataType.Force.ToUnit()}"; }
        public float EnemyPowerSet { set => _textDataEnemy.text = $"Enemy power :\t{value:00.0}"; }

        #endregion

    }
}
