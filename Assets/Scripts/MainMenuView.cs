using System;
using JoostenProductions;
using Profile;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MainMenuView : MonoBehaviour
{
    public event Action<TouchData> UpdateTouch;

    [SerializeField] private Button _buttonStart;
    [SerializeField] private GameObject _trail;

    public void Init(UnityAction startGame)
    {
        _buttonStart.onClick.AddListener(startGame);
        UpdateManager.SubscribeToUpdate(OnUpdate);
    }

    private void OnUpdate()
    {
        var touchCount = Input.touchCount;
        if (touchCount <= 0) return;
        
        for (int i = 0; i < touchCount; i++)
        {
            var touch = Input.GetTouch(i);

            UpdateTouch?.Invoke(new TouchData(touch));
        }
    }

    public GameObject CreateTrail(Vector3 position)
    {
        return Instantiate(_trail, position, Quaternion.identity);
    }

    protected void OnDestroy()
    {
        _buttonStart.onClick.RemoveAllListeners();
        UpdateManager.UnsubscribeFromUpdate(OnUpdate);
    }
}