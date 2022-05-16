using System;
using System.Collections.Generic;
using UnityEngine;


namespace Reward
{
    public class UIController : BaseController, IUIController
    {

        #region Fields

        private readonly RewardWindowView _rewardWindow;
        private readonly List<CurrencySO> _currencyList;
        private Dictionary<int, RewardCellView> _rewardCells;
        private Dictionary<Currency, StorageCellView> _storageCells;

        #endregion


        #region CodeLifeCycles

        public UIController(RewardWindowView rewardWindow, List<CurrencySO> currencyList)
        {
            _rewardWindow = rewardWindow;
            _currencyList = currencyList;

            CreateStorage();
        }

        #endregion


        #region Methods

        private void CreateStorage()
        {
            _storageCells = new Dictionary<Currency, StorageCellView>(_currencyList.Count);

            foreach (var currency in _currencyList)
            {
                _storageCells.Add(currency.CurrencyType, GameObject.Instantiate<StorageCellView>(_rewardWindow.StorageCell, _rewardWindow.StorageParent));
                _storageCells[currency.CurrencyType].Sprite = currency.CurrencySprite;
                AddGameObjects(_storageCells[currency.CurrencyType].gameObject);
            }
        }

        #endregion


        #region IUIController

        public void SetStorageItemValue(Currency currency, int value)
        {
            _storageCells[currency].Value = value;
        }

        public void CreateRewards(List<RewardData> rewardsList)
        {
            _rewardCells = new Dictionary<int, RewardCellView>(rewardsList.Count);

            foreach (var item in rewardsList)
            {
                _rewardCells.Add(item.Day, GameObject.Instantiate<RewardCellView>(_rewardWindow.RewardCell, _rewardWindow.RewardsParent));
                _rewardCells[item.Day].Sprite = _currencyList.Find(i => i.CurrencyType == item.Currency).CurrencySprite;
                _rewardCells[item.Day].Day = item.Day;
                _rewardCells[item.Day].Value = item.Value;
                AddGameObjects(_rewardCells[item.Day].gameObject);
            }
        }

        public void SetRewardItemActive(int day)
        {
            _rewardCells[day].ActivateReward = true;
        }

        public void SetTimer(DateTime time)
        {
            _rewardWindow.TimeToReward = time;
        }

        #endregion

    }
}