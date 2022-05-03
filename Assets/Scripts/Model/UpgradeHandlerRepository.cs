using System;
using System.Collections.Generic;

public class UpgradeHandlerRepository : BaseController
{
    public IReadOnlyDictionary<int, IUpgradeCarHandler> UpgradeItems => _upgradeItems;

    private Dictionary<int, IUpgradeCarHandler> _upgradeItems = new Dictionary<int, IUpgradeCarHandler>();

    public UpgradeHandlerRepository(IReadOnlyList<UpgradeItemConfig> configs)
    {
        PopulateItems(ref _upgradeItems, configs);
    }

    private void PopulateItems(ref Dictionary<int, IUpgradeCarHandler> upgradeItems, IReadOnlyList<UpgradeItemConfig> configs)
    {
        foreach (var config in configs)
        {
            upgradeItems[config.Id] = CreateHandler(config);
        }
    }

    private IUpgradeCarHandler CreateHandler(UpgradeItemConfig config)
    {
        switch (config.UpgradeType)
        {
            case UpgradeType.None:
                return UpgradeHandlerStub.Default;
            case UpgradeType.Speed:
                return new SpeedUpgradeHandler(config);
            case UpgradeType.Control:
                return UpgradeHandlerStub.Default;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}
