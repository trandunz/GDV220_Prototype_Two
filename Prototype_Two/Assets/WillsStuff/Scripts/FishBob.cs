using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishBob : MonoBehaviour
{
    
    int Direction = 1;

    Vector3 Velocity = Vector3.zero;

    [SerializeField] float HorizontalSpeed = 1.0f;
    [SerializeField] GameObject Mesh;

    public void SetXDirection(int _direction)
    {
        Direction = _direction;
    }

    private void Update()
    {
        Velocity.x = HorizontalSpeed * Direction;
        Velocity.y = Mathf.Sin(Time.time * HorizontalSpeed);
        RotateToDirection();
        transform.Translate(Velocity * Time.deltaTime);
    }

    void RotateToDirection()
    {
        var dir = Velocity.normalized;
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        var q = Quaternion.AngleAxis(angle, Vector3.back);
        Mesh.transform.rotation = q;
    }
}
