using RaceMobile.Base;
using RaceMobile.Reward;
using RaceMobile.Tools.ResourceManagment;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RaceMobile.Reward
{
    internal class DailyRewardController : BaseController
    {
        private readonly ResourcePath path = new ResourcePath() { PathResource = "Prefabs/Reward/DailyRewardWindow" };
        private readonly Transform placeForUI;

        private readonly ProfilePlayer profilePlayer;
        private readonly DailyRewardModel dailyRewardModel;

        private DailyRewardView dailyRewardView;
        private List<SlotRewardView> slotsForRewards;

        private bool rewardReceived;

        public DailyRewardController(Transform placeForUI, ProfilePlayer profilePlayer)
        {
            this.profilePlayer = profilePlayer;
            this.placeForUI = placeForUI;
            dailyRewardView = LoadView();
            dailyRewardModel = profilePlayer.dailyRewardModel;

            InitSlots();
            RefreshUI();
            dailyRewardView.StartCoroutine(UpdateCoroutine());
            SubscribeButton();


        }


        private IEnumerator UpdateCoroutine()
        {
            while (true)
            {
                Update();
                yield return new WaitForSeconds(1);
            }
        }


        private void Update()
        {
            RefreshRewardState();
            RefreshUI();
        }

        private void RefreshRewardState()
        {
            rewardReceived = false;
            if (profilePlayer.dailyRewardModel.LastRewardTime.HasValue)
            {
                var timeSpan = DateTime.UtcNow - profilePlayer.dailyRewardModel.LastRewardTime.Value;
                if(timeSpan.Seconds > dailyRewardView.TimeDeadline)
                {
                    profilePlayer.dailyRewardModel.LastRewardTime = null;
                    profilePlayer.dailyRewardModel.CurrentActiveSlot = 0;
                }
                else if(timeSpan.Seconds < dailyRewardView.TimeCooldown)
                {
                    rewardReceived = true;
                }
            }
        }

        private void RefreshUI()
        {
            dailyRewardView.GetRewardButton.interactable = !rewardReceived;

            for (int i = 0; i < dailyRewardView.Rewards.Count; i++)
            {
                slotsForRewards[i].SetData(dailyRewardView.Rewards[i], i + 1, i <= profilePlayer.dailyRewardModel.CurrentActiveSlot);
            }

            DateTime nextDailyBonusTime =
                !dailyRewardModel.LastRewardTime.HasValue
                ? DateTime.MinValue
                : dailyRewardModel.LastRewardTime.Value.AddSeconds(dailyRewardView.TimeCooldown);
            var delta = nextDailyBonusTime - DateTime.UtcNow;
            if (delta.TotalSeconds < 0)
                delta = new TimeSpan(0);
            dailyRewardView.RewardTimer.text = delta.ToString();


        }

        private void ClaimReward()
        {
            if (rewardReceived)
                return;
            var reward = dailyRewardView.Rewards[profilePlayer.dailyRewardModel.CurrentActiveSlot];
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
                default:
                    throw new ArgumentOutOfRangeException();
            }

            profilePlayer.dailyRewardModel.LastRewardTime = DateTime.UtcNow;
            profilePlayer.dailyRewardModel.CurrentActiveSlot = (profilePlayer.dailyRewardModel.CurrentActiveSlot + 1) % dailyRewardView.Rewards.Count;
            RefreshRewardState();

            Dispose();
        }

        private void CloseWindow()
        {
            Dispose();
        }



        private void SubscribeButton()
        {
            dailyRewardView.GetRewardButton.onClick.AddListener(ClaimReward);
            dailyRewardView.CloseWindowButton.onClick.AddListener(CloseWindow);
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