using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class MainWindowObserver : MonoBehaviour
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

    private Power playerPower;
    private Health playerHealth;
    private Money playerMoney;

    private Enemy enemy;


    // Start is called before the first frame update
    void Start()
    {
        playerHealth = new Health(nameof(Health));
        playerMoney = new Money(nameof(Money));
        playerPower = new Power(nameof(Power));

        enemy = new Enemy("Enemy Flappy");
        playerMoney.Attach(enemy);
        playerHealth.Attach(enemy);
        playerPower.Attach(enemy);

        addCoinButton.onClick.AddListener(() => ChangeMoney(true));
        minusCoinButton.onClick.AddListener(() => ChangeMoney(false));

        addHealthButton.onClick.AddListener(() => ChangeHealth(true));
        minusHealthButton.onClick.AddListener(() => ChangeHealth(false));

        addForceButton.onClick.AddListener(() => ChangePower(true));
        minusForceButton.onClick.AddListener(() => ChangePower(false));

        fightButton.onClick.AddListener(Fight);
        
    }

    private void OnDestroy()
    {
        addCoinButton.onClick.RemoveAllListeners();
        minusCoinButton.onClick.RemoveAllListeners();

        addForceButton.onClick.RemoveAllListeners();
        minusForceButton.onClick.RemoveAllListeners();

        addHealthButton.onClick.RemoveAllListeners();
        minusHealthButton.onClick.RemoveAllListeners();

        playerMoney.Detach(enemy);
        playerHealth.Detach(enemy);
        playerPower.Detach(enemy);
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
}
