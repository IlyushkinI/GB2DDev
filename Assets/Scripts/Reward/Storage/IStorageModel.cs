using System;


namespace Reward
{
    public interface IStorageModel
    {
        DateTime WhenCollectingAvailable { get; set; }
        int CurrentRewardItemID { get; set; }
        void SetCurrency(Currency currency, int value);
        int GetCurrency(Currency currency);
        void SetDefaults();
    }
}