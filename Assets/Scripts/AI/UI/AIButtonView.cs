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
        private AIUIElements _eventCaller;

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
            _eventSO?.Invoke(_eventCaller);
        }

        #endregion

    }
}
