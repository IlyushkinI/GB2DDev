using System;


namespace Reward
{
    public interface IStorageModel
    {
        DateTime WhenCollectingAvailable { get; set; }
        int CurrentRewardItem { get; set; }
        void SetCurrency(Currency currency, int value);
        int GetCurrency(Currency currency);
    }
}