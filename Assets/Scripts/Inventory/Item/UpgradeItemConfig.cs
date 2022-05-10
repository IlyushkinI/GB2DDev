using UnityEngine;

[CreateAssetMenu(fileName = "UpgradeItem", menuName = "UpgradeItem")]
public class UpgradeItemConfig : ScriptableObject
{

    #region Fields

    [SerializeField]
    private string _name;

    [SerializeField]
    private ItemConfig _itemConfig;

    [SerializeField]
    private UpgradeType _upgradeType;

    [SerializeField]
    private float _valueUpgrade;

    #endregion


    #region Properties

    public string Name => _name;

    public int Id => _itemConfig.Id;

    public UpgradeType UpgradeType => _upgradeType;

    public float ValueUpgrade => _valueUpgrade;

    #endregion

}
