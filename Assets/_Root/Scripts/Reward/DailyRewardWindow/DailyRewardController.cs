using RaceMobile.Base;
using RaceMobile.Reward;
using RaceMobile.Tools.ResourceManagment;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace RaceMobile.Reward
{
    internal class DailyRewardController : BaseController
    {
        private readonly ResourcePath path = new ResourcePath() { PathResource = "Prefabs/Reward/DailyRewardWindow" };
        private DailyRewardView dailyRewardView;
        private readonly Transform placeForUI;
        private readonly ProfilePlayer profilePlayer;

        private readonly DailyRewardModel dailyRewardModel;

        private List<SlotRewardView> slotsForRewards;

        public DailyRewardController(Transform placeForUI, ProfilePlayer profilePlayer)
        {
            this.profilePlayer = profilePlayer;
            this.placeForUI = placeForUI;
            dailyRewardView = LoadView();

            SubscribeButton();
            InitSlots();
        }

        private void SubscribeButton()
        {
            dailyRewardView.GetRewardButton.onClick.AddListener(GetReward);
        }

        private void GetReward()
        {
            var reward = dailyRewardView.Rewards[dailyRewardModel.CurrentActiveSlot];

            switch (reward.RewardType)
            {
                case RewardType.None:
                    break;
                case RewardType.Silver:
                    profilePlayer.CurrencyModel.Silver += reward.Count;
                    break;
                case RewardType.Gold:
                    profilePlayer.CurrencyModel.Gold += reward.Count;
                    break;                
            }

            Dispose();
        }

        private void InitSlots()
        {
            slotsForRewards = new List<SlotRewardView>();
            for (int i = 0; i < dailyRewardView.Rewards.Count; i++)
            {
                var reward = dailyRewardView.Rewards[i];
                var slotInstance = GameObject.Instantiate(dailyRewardView.SlotPrefab, dailyRewardView.SlotsParent, false);
                slotInstance.SetData(reward, i + 1, false);
                slotsForRewards.Add(slotInstance);
            }
        }

        private DailyRewardView LoadView()
        {
            var pref = ResourceLoader.LoadPrefab(path);
            var go = GameObject.Instantiate(pref, placeForUI);
            AddGameObject(go);

            return go.GetComponent<DailyRewardView>();
        }


    }
}