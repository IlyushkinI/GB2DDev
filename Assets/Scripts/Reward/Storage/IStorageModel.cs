namespace Reward
{
    public interface IStorageModel
    {
        int CurrentRewardItem { get; set; }
        void SetCurrency(Currency currency, int value);
        int GetCurrency(Currency currency);
    }
}