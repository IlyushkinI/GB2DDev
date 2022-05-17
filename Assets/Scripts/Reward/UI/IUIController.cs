using System;
using System.Collections.Generic;


namespace Reward
{
    public interface IUIController
    {
        bool SetButtonGetRewardInteractable { set; }
        void SetStorageItemValue(Currency currency, int value);
        void CreateRewards(List<RewardData> rewardsList);
        void SetRewardItemActive(int day, bool state);
        void SetRewardItemCollectedState(int day, bool state);
        void SetTimer(DateTime time);
    }

}