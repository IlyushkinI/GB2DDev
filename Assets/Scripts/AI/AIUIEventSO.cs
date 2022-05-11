using System;
using UnityEngine;


namespace AI
{
    [CreateAssetMenu]
    public sealed class AIUIEventSO : ScriptableObject
    {

        private Action<AIUIElement, PlayerDataType, int> _eventAction;

        public event Action<AIUIElement, PlayerDataType, int> UIEvent
        {
            add { _eventAction += value; }
            remove { _eventAction -= value; }
        }

        public void Invoke(AIUIElement eventCaller, PlayerDataType dataType, int value) => _eventAction.Invoke(eventCaller, dataType, value);

    }
}
