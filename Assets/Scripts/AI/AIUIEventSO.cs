using System;
using UnityEngine;


namespace AI
{
    [CreateAssetMenu]
    public sealed class AIUIEventSO : ScriptableObject
    {

        private Action<AIUIElements> _eventAction;

        public event Action<AIUIElements> UIEvent
        {
            add { _eventAction += value; }
            remove { _eventAction -= value; }
        }

        public void Invoke(AIUIElements eventCaller) => _eventAction.Invoke(eventCaller);

    }
}
