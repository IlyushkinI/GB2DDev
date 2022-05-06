using JoostenProductions;
using Tools;
using UnityEngine;


internal sealed class InputButtonView : BaseInputView
{

    #region enum

    private enum Direction
    {
        None = 0,
        Left = 1,
        Right = 2,
    }

    #endregion


    #region Fields

    [Space]
    [SerializeField]
    private ButtonHoldView _leftButton;

    [Space]
    [SerializeField]
    private ButtonHoldView _rightButton;

    private Direction _direction;

    #endregion


    #region UnityMethods

    private void OnDestroy()
    {
        _leftButton.ButtonState -= OnLeftPress;
        _rightButton.ButtonState -= OnRightPress;
        UpdateManager.UnsubscribeFromUpdate(DoItInUpdate);
    }

    #endregion


    #region Methods

    public override void Init(SubscriptionProperty<float> leftMove, SubscriptionProperty<float> rightMove, float speed)
    {
        base.Init(leftMove, rightMove, speed);
        UpdateManager.SubscribeToUpdate(DoItInUpdate);
        _leftButton.ButtonState += OnLeftPress;
        _rightButton.ButtonState += OnRightPress;
    }

    private void OnLeftPress(bool isPressed)
    {
        if (_direction != Direction.Right)
        {
            _direction = isPressed ? Direction.Left : Direction.None;
        }
    }

    private void OnRightPress(bool isPressed)
    {
        if (_direction != Direction.Left)
        {
            _direction = isPressed ? Direction.Right : Direction.None;
        }
    }

    private void DoItInUpdate()
    {
        if (_direction != Direction.None)
        {
            if (_direction == Direction.Right)
            {
                OnRightMove(_speed * Time.deltaTime);
            }
            else
            {
                OnLeftMove(-_speed * Time.deltaTime);
            }
        }
    }

    #endregion

}
