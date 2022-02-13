using UnityEngine;

[CreateAssetMenu(fileName = "UpgradeItem", menuName = "UpgradeItem")]
public class UpgradeItemConfig : ScriptableObject
{
    [SerializeField]
    private ItemConfig _itemConfig;

    [SerializeField]
    private UpgradeType _upgradeType;

    [SerializeField]
    private int _valueUpgrade;

    [SerializeField]
    private Sprite _spriteOfItem;

    public int Id => _itemConfig.Id;

    public UpgradeType UpgradeType => _upgradeType;

    public int ValueUpgrade => _valueUpgrade;

    public Sprite SpriteOfItem => _spriteOfItem;

    public bool IsEquiped { get; set; }
}

public enum UpgradeType
{
    None,
    Speed,
    Control
}
