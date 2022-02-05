using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JoostenProductions;
using Tools;


public class SliceInputView : BaseInputView
{
    private float _speed = 0.0f;
    private float _tapAcceleration = 0.1f;
    private float _slowUpPerSecond = 0.5f;

    public override void Init(SubscriptionProperty<float> leftMove, SubscriptionProperty<float> rightMove, float speed)
    {
        base.Init(leftMove, rightMove, speed);
        UpdateManager.SubscribeToUpdate(OnUpdate);
    }

    private void OnUpdate()
    {
        if (Input.touchCount > 0)
        {
            var touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Moved)
            {
                if (touch.deltaPosition.x > 0)
                {
                    AddAcceleration(_tapAcceleration);
                }

                else if (touch.deltaPosition.x < 0)
                {
                    AddAcceleration(-_tapAcceleration);
                }
            }
        }

        Move();
        Slowdown();
    }

    private void AddAcceleration(float acc)
    {
        _speed = Mathf.Clamp(_speed + acc, -1f, 1f);
    }

    private void Move()
    {
        if (_speed < 0)
            OnLeftMove(_speed);
        else
            OnRightMove(_speed);
    }

    private void Slowdown()
    {
        var sgn = Mathf.Sign(_speed);
        _speed = Mathf.Clamp01(Mathf.Abs(_speed) - _slowUpPerSecond * Time.deltaTime) * sgn;
    }

    void OnDestroy()
    {
        UpdateManager.UnsubscribeFromUpdate(OnUpdate);
    }
}
