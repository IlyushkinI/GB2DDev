using System;
using UnityEngine;


namespace Reward
{
    [CreateAssetMenu(menuName = "Reward/UIEventSO", fileName = "RewardEventsUI")]
    public sealed class UIEventSO : ScriptableObject
    {

        private Action<UIElement> _eventAction;

        public event Action<UIElement> UIEvent
        {
            add { _eventAction += value; }
            remove { _eventAction -= value; }
        }

        public void Invoke(UIElement eventCaller) => _eventAction?.Invoke(eventCaller);

    }
}
