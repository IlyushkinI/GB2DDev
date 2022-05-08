using System.Collections.Generic;


public interface IUpgradeableCar
{
    List<UpgradeItemConfig> AppliedItems { get; set; }
    float Speed { get; set; }
    float Control { get; set; }
    void Restore();
}