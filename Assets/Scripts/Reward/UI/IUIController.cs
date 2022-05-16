using System;
using System.Collections.Generic;


namespace Reward
{
    public interface IUIController
    {
        void SetStorageItemValue(Currency currency, int value);
        void CreateRewards(List<RewardData> rewardsList);
        void SetRewardItemActive(int day);
        void SetTimer(DateTime time);
    }

}