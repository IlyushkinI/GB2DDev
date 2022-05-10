using JoostenProductions;
using Tools;
using UnityEngine;

public sealed class InputSwipeController : BaseController
{

    #region Fields

    private SubscriptionProperty<float> _leftMove;
    private SubscriptionProperty<float> _rightMove;

    private readonly float _speed;

    private bool _isFirstFrame = true;
    private Vector3 _pointerPositionInLastFrame;

    private DelegateForUpdate _delegateUpdate;

    #endregion


    #region CodeLifeCycles

    public InputSwipeController(SubscriptionProperty<float> leftMove, SubscriptionProperty<float> rightMove, float speed)
    {
        _leftMove = leftMove;
        _rightMove = rightMove;
        _speed = speed;

        if (Input.mousePresent)
        {
            _delegateUpdate = MouseTouch;
        }
        else
        {
            _delegateUpdate = FingerTouch;
        }

        UpdateManager.SubscribeToUpdate(DoItInUpdate);
        AddController(this);
    }

    #endregion


    #region Methods

    delegate void DelegateForUpdate();

    private void DoItInUpdate()
    {
        _delegateUpdate();
    }

    private void FingerTouch()
    {
        CheckMove(Input.touchCount > 0, Input.GetTouch(0).position, ref _pointerPositionInLastFrame);
    }

    private void MouseTouch()
    {
        CheckMove(Input.GetMouseButton(0), CorrectPosition(Input.mousePosition), ref _pointerPositionInLastFrame);
    }

    private void CheckMove(bool isInput, Vector3 pointerPosition, ref Vector3 pointerPositionInLastFrame)
    {
        if (isInput)
        {
            if (_isFirstFrame)
            {
                pointerPositionInLastFrame = pointerPosition;
                _isFirstFrame = false;
            }
            else
            {
                if (pointerPosition.x > pointerPositionInLastFrame.x)
                {
                    _rightMove.Value = _speed * Time.deltaTime;
                }
                else
                {
                    if (pointerPosition.x < pointerPositionInLastFrame.x)
                    {
                        _leftMove.Value = -_speed * Time.deltaTime;
                    }
                }
            }
        }
        else
        {
            _isFirstFrame = true;
            _leftMove.Value = 0.0f;
            _rightMove.Value = 0.0f;
        }
    }

    private Vector3 CorrectPosition(Vector3 position)
    {
        return new Vector3(position.x - 552.5f, position.y - 294.5f, 0.0f) / 50.0f;
    }

    protected override void OnDispose()
    {
        UpdateManager.UnsubscribeFromUpdate(DoItInUpdate);
    }

    #endregion

}
