using UnityEngine;


public class CarView : MonoBehaviour
{

    #region Fields

    [SerializeField]
    private SpriteRenderer _backWheel;

    [SerializeField]
    private SpriteRenderer _frontWheel;

    #endregion


    #region Properties

    public SpriteRenderer BackWheel => _backWheel;

    public SpriteRenderer FrontWheel => _frontWheel;

    #endregion

}
