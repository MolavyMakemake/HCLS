using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public Aim aim;
    public Gun gun;
    public Controller controller;

    float _lastShot = -9999;
    bool _fire = false;

    private void Update()
    {
        _fire = controller.GetFireInput();
    }

    private void FixedUpdate()
    {
        if (_fire && Time.time - _lastShot > gun.fireRate)
        {
            if (gun.Fire());
                _lastShot = Time.time;
        }
    }
}
