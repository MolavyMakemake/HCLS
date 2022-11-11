using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public Aim aim;
    public Gun gun;
    public Controller controller;

    public float recoilT = 0;

    float _lastShot = -9999;
    bool _fire = false;

    public ParticleSystem[] particles;

    private void Update()
    {
        _fire = controller.GetPrimaryInput();
    }

    private void FixedUpdate()
    {
        if (_fire && Time.time - _lastShot > gun.fireRate)
        {
            if (gun.Fire(transform.position, aim.direction))
            {
                foreach (var ps in particles)
                    ps.Play();

                _lastShot = Time.time;


                Vector2 recoil = new Vector2(
                    gun.recoilX.Evaluate(recoilT),
                    gun.recoilY.Evaluate(recoilT)
                    );

                aim.Recoil(recoil);
                recoilT = Mathf.MoveTowards(recoilT, 1, gun.fireRate);
            }
        }
        
        if (!_fire)
            recoilT = Mathf.MoveTowards(recoilT, 0, Time.fixedDeltaTime);
    }
}
