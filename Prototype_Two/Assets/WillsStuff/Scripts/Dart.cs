using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dart : MonoBehaviour
{
    [SerializeField] float MoveSpeed = 1.0f;
    Vector3 MoveDirection = Vector3.down;

    public void SetDirection(Vector3 _direction)
    {
        MoveDirection = _direction;
    }

    private void Update()
    {
        var dir = MoveDirection.normalized;
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        var q = Quaternion.AngleAxis(angle + 90.0f, Vector3.forward);
        transform.rotation = q;
        transform.position += MoveDirection.normalized * MoveSpeed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag != "Dart"
            && other.gameObject.tag != "Player")
        {
            Destroy(gameObject);
        }
    }
}
