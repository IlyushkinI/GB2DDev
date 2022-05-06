public interface IUpgradeableCar
{
    float Speed { get; set; }
    float Control { get; set; }
    void Restore();
}