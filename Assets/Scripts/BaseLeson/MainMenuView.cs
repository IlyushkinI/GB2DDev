using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.Purchasing;
using System;

public class MainMenuView : MonoBehaviour
{
    [SerializeField] private Button _buttonStart;
    [SerializeField] private Button _buttonBuy;
    [SerializeField] private IAPButton _buttonPuchase;
    [SerializeField] private Text _moneyPlayer;


    public void Init(UnityAction startGame, UnityAction buy)
    {
        _moneyPlayer.text = 0.ToString();
        _buttonPuchase.onPurchaseComplete.AddListener(AddMoney);//
        _buttonStart.onClick.AddListener(startGame);
       // _buttonBuy.onClick.AddListener(buy);
       
    }

    private void AddMoney(Product product)
    {
        var money = Int32.Parse(_moneyPlayer.text) + 1;
        _moneyPlayer.text = money.ToString();
 
    }

    protected void OnDestroy()
    {
        _buttonBuy.onClick.RemoveAllListeners();
        _buttonStart.onClick.RemoveAllListeners();
    }
}