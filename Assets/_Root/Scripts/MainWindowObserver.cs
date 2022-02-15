using UnityEngine.UI;
using UnityEngine;
using TMPro;
using System;

public class MainWindow : MonoBehaviour
{
    [SerializeField] private TMP_Text playerMoneyText;
    [SerializeField] private TMP_Text playerHealthText;
    [SerializeField] private TMP_Text playerPowerText;
    [SerializeField] private TMP_Text enemyPowerText;

    [SerializeField] Button addCoinButton;
    [SerializeField] Button minusCoinButton;

    [SerializeField] Button addHealthButton;
    [SerializeField] Button minusHealthButton;

    [SerializeField] Button addForceButton;
    [SerializeField] Button minusForceButton;

    [SerializeField] Button fightButton;

    private int allCountMoneyPlayer;
    private int allCountHealthPlayer;
    private int allCountPowerPlayer;

    private DataPlayer dataPlayer;
    private Enemy enemy;


    // Start is called before the first frame update
    void Start()
    {
        dataPlayer = new DataPlayer("data player");
        enemy = new Enemy("Enemy Flappy");
        dataPlayer.Attach(enemy);

        addCoinButton.onClick.AddListener(() => ChangeMoney(true));
        minusCoinButton.onClick.AddListener(() => ChangeMoney(false));

        addHealthButton.onClick.AddListener(() => ChangeHealth(true));
        minusHealthButton.onClick.AddListener(() => ChangeHealth(false));

        addForceButton.onClick.AddListener(() => ChangePower(true));
        minusForceButton.onClick.AddListener(() => ChangePower(false));

        fightButton.onClick.AddListener(Fight);
        
    }

    private void Fight()
    {
        Debug.Log(allCountPowerPlayer >= enemy.Power
            ? "<color = #07FF00>Win!!!</color>"
            : "<color = #FF0000>Loose!!!</color>");
    }

    private void ChangeDataWindow(int countChangeData, DataType dataType)
    {
        switch (dataType)
        {
            case DataType.Money:
                playerMoneyText.text = $"Player Money {countChangeData.ToString()}";
                dataPlayer.CountMoney = allCountMoneyPlayer;
                break;

            case DataType.Health:
                playerHealthText.text = $"Player Health {countChangeData.ToString()}";
                dataPlayer.CountHealth = countChangeData;
                break;
            case DataType.Power:
                playerPowerText.text = $"Player Power {countChangeData.ToString()}";
                dataPlayer.CountPower = countChangeData;
                break;
        }


    }

    private void ChangeMoney(bool isAddCount)
    {
        if (isAddCount)
            allCountMoneyPlayer++;
        else
            allCountMoneyPlayer--;
        ChangeDataWindow(allCountMoneyPlayer, DataType.Money);
    }

    private void ChangeHealth(bool isAddCount)
    {
        if (isAddCount)
            allCountHealthPlayer++;
        else
            allCountHealthPlayer--;
        ChangeDataWindow(allCountHealthPlayer, DataType.Health);
    }

    private void ChangePower(bool isAddCount)
    {
        if (isAddCount)
            allCountPowerPlayer++;
        else
            allCountPowerPlayer--;
        ChangeDataWindow(allCountPowerPlayer, DataType.Power);
    }
}
