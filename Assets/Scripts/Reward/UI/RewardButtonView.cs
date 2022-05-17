using UnityEngine;
using UnityEngine.UI;


namespace Reward
{
    public sealed class RewardButtonView : MonoBehaviour
    {

        #region Fields

        [SerializeField]
        private UIEventSO _eventSO;

        [Space]
        [SerializeField]
        private Button _button;

        [Space]
        [SerializeField]
        private UIElement _eventCaller;

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
