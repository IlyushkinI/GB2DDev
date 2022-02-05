using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JoostenProductions;
using Tools;
using UnityEngine;

public class SliceInMainMenuView : MonoBehaviour
{
    [SerializeField] private TrailRenderer _trail;

    private ResourcePath _path = new ResourcePath() { PathResource = "SliceParametresInMainMenu" };
    private Vector3 _position;

    public void Initialize()
    {
        UpdateManager.SubscribeToUpdate(OnUpdate);
        var dataSlice = ResourceLoader<ScriptableSlicer>.LoadPrefab(_path);
        _trail.gameObject.transform.position = Camera.main.transform.position;
        _position = _trail.gameObject.transform.position;
        _trail.widthCurve = dataSlice.animationCurve;
        _trail.time = dataSlice.time;
    }

    private void OnUpdate()
    {
        if (Input.touchCount > 0)
        {
            var touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Moved)
            {
                var pos = Camera.main.ScreenToWorldPoint(touch.position);
                _position.Set(pos.x, pos.y, 0);
                _trail.gameObject.transform.position = _position;
                _trail.enabled = true;
            }
            if (touch.phase == TouchPhase.Ended)
            {
                _trail.enabled = false;
            }
        }
    }



    void OnDestroy()
    {
        UpdateManager.UnsubscribeFromUpdate(OnUpdate);
    }
}
