using TMPro;
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

        [SerializeField]
        private TextMeshProUGUI _textOnButton;

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
            if (_dataType != PlayerDataType.None)
            {
                _textOnButton.text = $"{((_dataValue > 0) ? "+ " : "- ")} {Mathf.Abs(_dataValue)} {_dataType.ToUnit()}";
            }

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
