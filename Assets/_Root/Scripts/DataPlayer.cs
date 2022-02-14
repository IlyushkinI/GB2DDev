using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal class DataPlayer
{
    private int countMoney;
    private int countHealth;
    private int countPower;

    private string titleData;
    private List<IEnemy> enemies;
    public string TitleData { get => titleData; }

    public DataPlayer(string titleData)
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
