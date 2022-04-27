using System.Collections.Generic;
using UnityEngine;


public sealed class ButtonStoreView : MonoBehaviour
{

    private List<SpriteRenderer> _wheels;

    public void PurchaseComplete()
    {
        foreach (var wheel in _wheels)
        {
            wheel.color = Color.green;
        }
    }

    private void OnEnable()
    {
        _wheels = new List<SpriteRenderer>();

        foreach (var wheel in GameObject.FindGameObjectsWithTag("Player"))
        {
            _wheels.Add(wheel.GetComponent<SpriteRenderer>());
        }
    }
}
