using UnityEngine.UI;
using UnityEngine;
using TMPro;
using System;

public class MainWindowObserver : MonoBehaviour
{
    [SerializeField] private TMP_Text playerMoneyText;
    [SerializeField] private TMP_Text playerHealthText;
    [SerializeField] private TMP_Text playerPowerText;
    [SerializeField] private TMP_Text playerCrimeText;
    [SerializeField] private TMP_Text enemyPowerText;

    [SerializeField] Button addCrimeButton;
    [SerializeField] Button minusCrimeButton;

    [SerializeField] Button addCoinButton;
    [SerializeField] Button minusCoinButton;

    [SerializeField] Button addHealthButton;
    [SerializeField] Button minusHealthButton;

    [SerializeField] Button addForceButton;
    [SerializeField] Button minusForceButton;

    [SerializeField] Button fightButton;
    [SerializeField] Button fightSkipButton;
    
    private int allCountMoneyPlayer;
    private int allCountHealthPlayer;
    private int allCountPowerPlayer;
    private int allCountCrimePlayer;

    private Power playerPower;
    private Health playerHealth;
    private Money playerMoney;
    private Crime playerCrime;

    private Enemy enemy;
    private Knack knack;


    // Start is called before the first frame update
    void Start()
    {
        playerHealth = new Health(nameof(Health));
        playerMoney = new Money(nameof(Money));
        playerPower = new Power(nameof(Power));
        playerCrime = new Crime(nameof(Crime));

        enemy = new Enemy("Enemy Flappy");
        knack = new Knack(fightSkipButton);

        playerCrime.Attach(knack);
        playerMoney.Attach(enemy);
        playerHealth.Attach(enemy);
        playerPower.Attach(enemy);
        playerCrime.Attach(enemy);

        addCoinButton.onClick.AddListener(() => ChangeMoney(true));
        minusCoinButton.onClick.AddListener(() => ChangeMoney(false));

        addHealthButton.onClick.AddListener(() => ChangeHealth(true));
        minusHealthButton.onClick.AddListener(() => ChangeHealth(false));

        addForceButton.onClick.AddListener(() => ChangePower(true));
        minusForceButton.onClick.AddListener(() => ChangePower(false));

        addCrimeButton.onClick.AddListener(() => ChangeCrime(true));
        minusCrimeButton.onClick.AddListener(() => ChangeCrime(false));

        fightButton.onClick.AddListener(Fight);
        fightSkipButton.onClick.AddListener(Skip);
        
    }

    private void Skip()
    {
        Debug.Log("<color=#E7E008>Go away!!!</color>");
    }

    private void OnDestroy()
    {
        addCoinButton.onClick.RemoveAllListeners();
        minusCoinButton.onClick.RemoveAllListeners();

        addForceButton.onClick.RemoveAllListeners();
        minusForceButton.onClick.RemoveAllListeners();

        addHealthButton.onClick.RemoveAllListeners();
        minusHealthButton.onClick.RemoveAllListeners();

        addCrimeButton.onClick.RemoveAllListeners();
        minusCrimeButton.onClick.RemoveAllListeners();

        playerMoney.Detach(enemy);
        playerHealth.Detach(enemy);
        playerPower.Detach(enemy);
        playerCrime.Detach(enemy);
        playerCrime.Detach(knack);
    }

    private void Fight()
    {
        Debug.Log(allCountPowerPlayer >= enemy.Power
            ? "<color=#07FF00>Win!!!</color>"
            : "<color=#FF0000>Loose!!!</color>");
    }

    private void ChangeDataWindow(int countChangeData, DataType dataType)
    {
        switch (dataType)
        {
            case DataType.Money:
                playerMoneyText.text = $"Player Money {countChangeData.ToString()}";
                playerMoney.CountMoney = allCountMoneyPlayer;
                break;

            case DataType.Health:
                playerHealthText.text = $"Player Health {countChangeData.ToString()}";
                playerHealth.CountHealth = countChangeData;
                break;
            case DataType.Power:
                playerPowerText.text = $"Player Power {countChangeData.ToString()}";
                playerPower.CountPower = countChangeData;
                break;
            case DataType.Crime:
                playerCrimeText.text = $"Player Crime {countChangeData.ToString()}";
                playerCrime.CountCrime = countChangeData;
                break;
        }

        enemyPowerText.text = $"Count Force {enemy.Power}";

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

    private void ChangeCrime(bool isAddCount)
    {
        if (isAddCount)
            allCountCrimePlayer++;
        else
            allCountCrimePlayer--;
        ChangeDataWindow(allCountCrimePlayer, DataType.Crime);
    }
}
