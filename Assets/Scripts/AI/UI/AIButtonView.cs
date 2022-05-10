using UnityEngine;
using UnityEngine.UI;


namespace AI
{
    public sealed class AIButtonView : MonoBehaviour
    {

        #region Fields

        [SerializeField]
        private AIUIEventSO _eventSO;

        [Space]
        [SerializeField]
        private Button _button;

        [Space]
        [SerializeField]
        private AIUIElement _eventCaller;

        [Space]
        [SerializeField]
        private PlayerDataType _dataType;

        [SerializeField]
        private int _dataValue;

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

        private void OnClick()
        {
            _eventSO?.Invoke(_eventCaller, _dataType, _dataValue);
        }

        #endregion

    }
}
