using System.Collections.Generic;


public class Car : IUpgradeableCar
{

    #region Fields

    private float _defaultSpeed;
    private float _defaultControl;
    private List<UpgradeItemConfig> _appliedItems;

    #endregion


    #region Properties

    public float Speed { get; set; }
    public float Control { get; set; }
    public List<UpgradeItemConfig> AppliedItems
    {
        get => _appliedItems;
        set => _appliedItems = new List<UpgradeItemConfig>(value);
    }

    #endregion


    #region CodeLifeCycles

    public Car(float speed)
    {
        _defaultSpeed = speed;
        _defaultControl = 0f;
        _appliedItems = new List<UpgradeItemConfig>();
        Restore();
    }

    #endregion


    #region IUpgradeableCar

    public void Restore()
    {
        Speed = _defaultSpeed;
        Control = _defaultControl;
    }

    #endregion

}