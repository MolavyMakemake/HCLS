using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aim : MonoBehaviour
{
    public Look look;
    public Controller controller;

    public LineRenderer debug;
    public Vector3 direction;
    public Vector3 target;

    public float spread;

    bool _isADS;
    SecondOrderDynamics interpolation;

    public void Recoil(Vector2 recoil)
    {
        const float cos30 = 0.866f;

        float a = Random.value * 2 * Mathf.PI;
        recoil += spread * new Vector2(Mathf.Cos(a), Mathf.Sin(a));

        Vector3 x = transform.right;
        Vector3 delta = new Vector3(
            x.x * recoil.x,
            recoil.y,
            x.z * recoil.x
            );

        //target += delta;
        look.crosshair.position += 50 * new Vector2(delta.x, cos30 * delta.y);
    }

    // Update is called once per frame
    void Update()
    {
        _isADS = controller.GetSecondaryInput();
        spread = Mathf.MoveTowards(spread, _isADS ? 0.05f : 0.2f, Time.deltaTime * 0.5f);
        look.crosshair.SetSpreadIndicator(spread);
    }

    private void FixedUpdate()
    {
        Vector3 position = transform.position;
        position.y = 1;

        direction = interpolation.Update(Time.fixedDeltaTime, target.normalized);

        debug.SetPosition(0, position);
        debug.SetPosition(1, position + direction * 5);

        transform.forward = direction;

        target = _isADS ? (look.target - position).normalized : look.normal;
    }

    private void OnEnable()
    {
        target = transform.forward;
        interpolation = new SecondOrderDynamics(4.58f, 0.35f, 0, target);
    }
}
