public class UpgradeHandlerStub : IUpgradeCarHandler
{
    public static IUpgradeCarHandler Default { get; } = new UpgradeHandlerStub();

    public IUpgradeableCar Upgrade(IUpgradeableCar car)
    {
        return car;
    }
}