using UnityEngine;
using UnityEngine.UI;


public class ButtonView : MonoBehaviour
{

    #region Fields

    [SerializeField]
    protected GlobalEventSO _eventSO;

    [Space]
    [SerializeField]
    private Button _button;

    [Space]
    [SerializeField]
    protected UIElements _eventCaller;

    #endregion


    #region UnityMethods

    private void OnEnable()
    {
        _button.onClick.AddListener(OnClick);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveAllListeners();
    }

    #endregion


    #region Methods

    protected virtual void OnClick()
    {
        _eventSO?.Invoke(_eventCaller);
    }

    #endregion

}
