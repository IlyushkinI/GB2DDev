using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal class Enemy : IEnemy
{
    private const int kCoins = 5;
    private const float kPower = 1.5f;
    private const int maxHealthPlayer = 20;

    private int healthPlayer;
    private int moneyPlayer;
    private int powerPlayer;

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
        }

        Debug.Log($"Notified {name} changed {dataPlayer}");
    }

    public int Power
    {
        get
        {
            var kHealth = maxHealthPlayer > healthPlayer ? 100 : 10;
            var power = (int)(moneyPlayer / kCoins + kHealth + powerPlayer / kPower);

            return power;
        }
    }
}
