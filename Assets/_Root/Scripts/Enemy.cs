using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal class Enemy : IObserver
{
    private const int kCoins = 5;
    private const float kPower = 1.5f;
    private const int maxHealthPlayer = 20;
    private const int thresholdCrime = 2;


    private int healthPlayer;
    private int moneyPlayer;
    private int powerPlayer;
    private int crimePlayer;

    private string name;

    public Enemy(string name)
    {
        this.name = name;
    }

    public void Update(DataPlayer dataPlayer, DataType dataType)
    {
        switch (dataType)
        {
            case DataType.Money:
                moneyPlayer = dataPlayer.CountMoney;
                break;
            case DataType.Health:
                healthPlayer = dataPlayer.CountHealth;
                break;
            case DataType.Power:
                powerPlayer = dataPlayer.CountPower;
                break;
            case DataType.Crime:
                crimePlayer = dataPlayer.CountCrime;
                break;
        }

        Debug.Log($"Notified {name} changed {dataPlayer}");
    }

    public int Power
    {
        get
        {
            var kHealth = healthPlayer > maxHealthPlayer? 100 : 10;
            var kCrime = crimePlayer > thresholdCrime ? 10 : 1;
            var power = (int)(moneyPlayer / kCoins + kHealth + powerPlayer / kPower + kCrime);

            return power;
        }
    }
}
