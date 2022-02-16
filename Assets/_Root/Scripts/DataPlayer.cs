using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal abstract class DataPlayer
{
    private int countMoney;
    private int countHealth;
    private int countPower;
    private int countCrime;

    private string titleData;
    private List<IObserver> enemies = new List<IObserver>();
    public string TitleData => titleData; 

    protected DataPlayer(string titleData)
    {
        this.titleData = titleData;
    }

    public int CountCrime
    {
        get => countCrime;
        set
        {
            if(countCrime != value)
            {
                countCrime = value;
                Notify(DataType.Crime);
            }
        }
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

    public void Attach(IObserver enemy)
    {
        enemies.Add(enemy);
    }

    public void Detach(IObserver enemy)
    {
        enemies.Remove(enemy);
    }

    public void Notify(DataType dataType)
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

internal class Crime : DataPlayer
{
    public Crime(string title) : base(title)
    {

    }
}
