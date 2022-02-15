using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal abstract class DataPlayer
{
    private int countMoney;
    private int countHealth;
    private int countPower;

    private string titleData;
    private List<IEnemy> enemies = new List<IEnemy>();
    public string TitleData => titleData; 

    protected DataPlayer(string titleData)
    {
        this.titleData = titleData;
    }

    public int CountMoney 
    { 
        get => countMoney; 
        set { 
            if(countMoney != value)
            {
                countMoney = value;
                Notify(DataType.Money);
            }
            
        } 
    }

    public int CountHealth
    {
        get => countHealth;
        set
        {
            if (countHealth != value)
            {
                countHealth = value;
                Notify(DataType.Health);
            }
        }
    }
    public int CountPower 
    {
        get => countPower;
        set
        {
            if (countPower != value)
            {
                countPower = value;
                Notify(DataType.Power);
            }
        }
    }

    public void Attach(IEnemy enemy)
    {
        enemies.Add(enemy);
    }

    public void Detach(IEnemy enemy)
    {
        enemies.Remove(enemy);
    }

    private void Notify(DataType dataType)
    {
        foreach (var enemy in enemies)
        {
            enemy.Update(this, dataType);
        }
    }
}

internal class Money : DataPlayer
{
    public Money(string title) : base(title)
    {

    }
}

internal class Health : DataPlayer
{
    public Health(string title) : base(title)
    {

    }
}

internal class Power : DataPlayer
{
    public Power(string title) : base(title)
    {

    }
}
