using TMPro;
using UnityEngine;
using UnityEngine.UI;


namespace AI
{
    public sealed class AISliderView : MonoBehaviour
    {

        #region Fields

        [SerializeField]
        private AIUIEventSO _eventSO;

        [Space]
        [SerializeField]
        private TextMeshProUGUI _title;

        [Space]
        [SerializeField]
        private Slider _slider;
        
        [SerializeField]
        private AIUIElement _eventCaller;

        [SerializeField]
        private PlayerDataType _dataType;

        #endregion


        #region UnityMethods

        private void OnEnable()
        {
            _title.text = SetTitle(0);
            _slider.value = 0.0f;
            _slider.onValueChanged.AddListener(OnValueChanged);
        }

        #endregion


        #region Methods

        private void OnValueChanged(float dataFloat)
        {
            int data = (int)dataFloat;

            _title.text = SetTitle(data);
            _eventSO.Invoke(_eventCaller, _dataType, data);
        }

        private string SetTitle(int value)
        {
            return $"{value}";
        }

        #endregion

    }
}