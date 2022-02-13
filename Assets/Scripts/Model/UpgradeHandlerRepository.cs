using System;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeHandlerRepository : BaseController
{
    public IReadOnlyDictionary<int, IUpgradeCarHandler> UpgradeItems => _upgradeItems;

    private Dictionary<int, IUpgradeCarHandler> _upgradeItems = new Dictionary<int, IUpgradeCarHandler>();

    public UpgradeHandlerRepository()
    {
    }

    public void PopulateItems(IReadOnlyList<UpgradeItemConfig> configs, ProfilePlayer car)
    {
        foreach (var config in configs)
        {
            if (config.IsEquiped)
            {
                _upgradeItems[config.Id] = CreateHandler(config, car);
            }
            
        }
    }

    private IUpgradeCarHandler CreateHandler(UpgradeItemConfig config, ProfilePlayer car)
    {
        switch (config.UpgradeType)
        {
            case UpgradeType.None:
                return UpgradeHandelrStub.Default;
            case UpgradeType.Speed:
                return new SpeedUpgradeHandler(config);
                break;
            case UpgradeType.Control:
                return new JumpUpgradeHandler(config, car);
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}

public interface IShedController
{
    void Enter();
    void Exit();
}