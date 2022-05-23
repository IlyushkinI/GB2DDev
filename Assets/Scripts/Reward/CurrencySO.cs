using UnityEngine;


namespace Reward
{
    [CreateAssetMenu(menuName = "Reward/Currency", fileName = "Currency")]
    public sealed class CurrencySO : ScriptableObject
    {

        #region Fields

        [SerializeField]
        private Currency _currencyType;

        [SerializeField]
        private Sprite _currencySprite;


        #endregion


        #region Properties

        public Currency CurrencyType => _currencyType;
        public Sprite CurrencySprite => _currencySprite;

        #endregion

    }
}
