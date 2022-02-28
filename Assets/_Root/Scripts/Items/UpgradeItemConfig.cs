using UnityEngine;

[CreateAssetMenu(fileName = "UpgradeItem", menuName = "MyConfigs/UpgradeItem", order = 1)]
internal class UpgradeItemConfig : ScriptableObject
{
    [SerializeField]
    private ItemConfig itemConfig;
    [SerializeField]
    private UpgradeType upgradeType;
    [SerializeField]
    private float value;

    public int ID => itemConfig.Id;

    public float Value => value;
    internal UpgradeType UpgradeType => upgradeType;
}


[CreateAssetMenu(fileName = "UpgradeItemConfigDataSource", menuName = "MyConfigs/UpgradeItemConfigDataSource", order = 2)]

internal class UpgradeItemConfigDataSource : ScriptableObject
{
    [SerializeField]
    private UpgradeItemConfig[] itemConfig;
    public UpgradeItemConfig[] ItemConfigs => itemConfig;

}
