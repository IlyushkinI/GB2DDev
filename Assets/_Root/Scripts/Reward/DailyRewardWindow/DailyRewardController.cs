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

        private List<SlotRewardView> slotsForRewards;

        public DailyRewardController(Transform placeForUI)
        {
            this.placeForUI = placeForUI;
            dailyRewardView = LoadView();

            InitSlots();
        }

        private void InitSlots()
        {
            slotsForRewards = new List<SlotRewardView>();
            for (int i = 0; i < dailyRewardView.rewards.Count; i++)
            {
                var reward = dailyRewardView.rewards[i];
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