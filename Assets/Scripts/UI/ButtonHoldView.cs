using System;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine;


[RequireComponent(typeof(Button))]
public class ButtonHoldView : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{

    #region Fields

    private bool _isPressed = false;
    private Action<bool> _buttonState;

    #endregion


    #region Properties

    public event Action<bool> ButtonState 
    {
        add { _buttonState += value; }
        remove { _buttonState -= value; }
    }

    #endregion


    #region IPointerDownHandler, IPointerUpHandler

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!_isPressed)
        {
            _isPressed = true;
            _buttonState?.Invoke(true);
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (_isPressed)
        {
            _isPressed = false;
            _buttonState?.Invoke(false);
        }
    }

    #endregion

}
