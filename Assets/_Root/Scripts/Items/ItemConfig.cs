using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "MyConfigs/Item", order = 0)]
public class ItemConfig : ScriptableObject
{
    [SerializeField]
    private int id;
    [SerializeField]
    private string title;

    public int Id { get => id; }
    public string Title { get => title; }
}

internal enum UpgradeType
{
    None,
    Speed,
    Health
}
