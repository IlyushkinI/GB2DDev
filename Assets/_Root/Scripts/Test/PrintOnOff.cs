using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class PrintOnOff : MonoBehaviour
{
    private void OnDisable()
    {
        Debug.Log("PrintOnDisable: script was disabled");
    }

    private void OnEnable()
    {
        Debug.Log("PrintOnEnable: script was enabled");
    }

    private void Update()
    {
#if UNITY_EDITOR
        Debug.Log("Editor causes this update");
#endif
    }
}
