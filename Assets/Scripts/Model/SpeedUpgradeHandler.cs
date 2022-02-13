using UnityEngine;
public class SpeedUpgradeHandler : IUpgradeCarHandler
{
    private readonly UpgradeItemConfig _config;

    public SpeedUpgradeHandler(UpgradeItemConfig config)
    {
        _config = config;
    }

    public IUpgradeableCar Upgrade(IUpgradeableCar car)
    {

        Debug.Log($"_config.ValueUpgrade = {_config.ValueUpgrade}");
        car.Speed += _config.ValueUpgrade;
        return car;
    }
}