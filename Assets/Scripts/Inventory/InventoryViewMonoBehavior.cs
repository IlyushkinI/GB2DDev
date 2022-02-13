using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using System.Linq;

public class InventoryViewMonoBehavior : MonoBehaviour
{
    [SerializeField] private Button _buttonClose;
    [SerializeField] private List<Button> _buttonsUpgrade = new List<Button>();

    public void Init(UnityAction closeInventory)
    {
        _buttonClose.onClick.AddListener(closeInventory);
        
    }

    public void InitUpgradeDetails(IReadOnlyList<UpgradeItemConfig> upgradeItems)
    {

        for(int i = 0; i< _buttonsUpgrade.Count; i++)
        {
            var j = i;
            if(i >= upgradeItems.Count)
            {
                return;
            }
            else
            {
                _buttonsUpgrade[i].GetComponent<Image>().sprite = upgradeItems[i].SpriteOfItem;
                _buttonsUpgrade[j].onClick.AddListener(() =>
                {
                    upgradeItems[j].IsEquiped = !upgradeItems[j].IsEquiped;
                    if (upgradeItems[j].IsEquiped)
                    {
                        Debug.Log("Equip");
                    }
                    else
                    {
                        Debug.Log("UnEquip");
                    }
                });
            }
        }
    }



    protected void OnDestroy()
    {
        _buttonClose.onClick.RemoveAllListeners();
        foreach(var button in _buttonsUpgrade)
        {
            button.onClick.RemoveAllListeners();
        }
        
    }
}
