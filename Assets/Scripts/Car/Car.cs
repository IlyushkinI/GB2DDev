public class Car: IUpgradeableCar
{
    public float Speed { get; set; }
    public float Control { get; set; }

    private float _defaultSpeed;
    private float _defaultControl;

    public Car(float speed)
    {
        _defaultSpeed = speed;
        _defaultControl = 0f;
        Restore();
    }

    public void Restore()
    {
        Speed = _defaultSpeed;
        Control = _defaultControl;
    }

}