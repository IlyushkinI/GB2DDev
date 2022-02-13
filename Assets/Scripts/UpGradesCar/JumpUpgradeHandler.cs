using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JoostenProductions;
using UnityStandardAssets.CrossPlatformInput;

public class JumpUpgradeHandler : IUpgradeCarHandler
{
    private readonly UpgradeItemConfig _config;
    private readonly ProfilePlayer _car;

    
    private float _currentTime;
    private const float DELTA_TIME = 2f;

    public JumpUpgradeHandler(UpgradeItemConfig config, ProfilePlayer car)
    {
        _car = car;
        _config = config;
        _currentTime = Time.time;
    }

    public IUpgradeableCar Upgrade(IUpgradeableCar car)
    {

        UpdateManager.SubscribeToUpdate(OnUpdate);
        return car;
    }

    private void OnUpdate()
    {
        
        if( _car != null)
        {
            var delta = Time.time - _currentTime;
            if (CrossPlatformInputManager.GetAxis("Vertical") == 1f && delta >= DELTA_TIME)
            {
                Debug.Log(CrossPlatformInputManager.GetAxis("Vertical"));
                _car.RigidbodyCar.AddForce(Vector2.up * _config.ValueUpgrade);
                _currentTime = Time.time;
            }
        }

    }
}
