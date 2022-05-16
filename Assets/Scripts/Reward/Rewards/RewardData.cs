namespace Reward
{
    public struct RewardData
    {

        #region Fields

        private int _day;
        private Currency _currency;
        private int _value;

        #endregion


        #region Properties

        public int Day { get => _day; set => _day = value; }
        public Currency Currency { get => _currency; set => _currency = value; }
        public int Value { get => _value; set => _value = value; }

        #endregion

    }
}