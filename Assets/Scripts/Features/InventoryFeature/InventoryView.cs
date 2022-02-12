﻿using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryView : IInventoryView
{
    private Transform _cellPlace;
    private GameObject _upgradeItemPref;
    private Action<UpgradeItem> _refreshInventory;
    public InventoryView(Action<UpgradeItem> refreshInventory)
    {
        _upgradeItemPref = (GameObject)Resources.Load("Prefabs/UpgradeItemView");
        _refreshInventory = refreshInventory;
    }

    public void Display(IReadOnlyList<UpgradeItem> items)
    {
        foreach(var item in items)
        {
            var cell = UnityEngine.Object.Instantiate(_upgradeItemPref, _cellPlace);
            var view = cell.GetComponent<UpgradeItemView>();
            view.Init(item, _refreshInventory);
        }            
    }

    public void Show()
    {
        throw new System.NotImplementedException();
    }

    public void Hide()
    {
        throw new System.NotImplementedException();
    }

    public void Init(Transform cellPlace)
    {
        _cellPlace = cellPlace;
    }
}
