using System;
using UnityEngine;


[CreateAssetMenu]
public class GlobalEventSO : ScriptableObject
{

    private Action<UIElements> _event;

    public event Action<UIElements> GlobalEventAction
    {
        add { _event += value; }
        remove { _event -= value; }
    }

    public void Invoke(UIElements eventCaller) => _event.Invoke(eventCaller);

}
