using System;
using UnityEngine;


[CreateAssetMenu]
public class GlobalEventSO : ScriptableObject
{

    private Action<UIElements> _eventAction;
    private Action<UIElements, int, string> _eventDropdown;

    public event Action<UIElements> GlobalEventAction
    {
        add { _eventAction += value; }
        remove { _eventAction -= value; }
    }

    public event Action<UIElements, int, string> GlobalEventDropdown
    {
        add { _eventDropdown += value; }
        remove { _eventDropdown -= value; }
    }

    public void Invoke(UIElements eventCaller) => _eventAction.Invoke(eventCaller);

    public void Invoke(UIElements eventCaller, int dropdownValue, string dropdownItemLabel) => _eventDropdown.Invoke(eventCaller, dropdownValue, dropdownItemLabel);

}
