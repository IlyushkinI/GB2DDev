using System;


namespace Reward
{
    public interface IStorageModel
    {
        DateTime WhenCollectingAvailable { get; set; }
        bool CurrentRewardCollectedState { get; set; }
        int CurrentRewardItemID { get; set; }
        int GetCurrency(Currency currency);
        void SetCurrency(Currency currency, int value);
        void SetDefaults();
    }
}