public class ControlUpgradeHandler : IUpgradeCarHandler
{

    private readonly UpgradeItemConfig _config;

    public ControlUpgradeHandler(UpgradeItemConfig config)
    {
        _config = config;
    }

    public IUpgradeableCar Upgrade(IUpgradeableCar car)
    {
        car.Control += _config.ValueUpgrade;
        return car;
    }

}