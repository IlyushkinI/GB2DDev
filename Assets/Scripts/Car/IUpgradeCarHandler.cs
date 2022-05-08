public interface IUpgradeCarHandler
{
    IUpgradeableCar Upgrade(IUpgradeableCar car);
    IUpgradeableCar Unupgrade(IUpgradeableCar car);
}