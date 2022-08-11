using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterStreaks : MonoBehaviour
{
    [SerializeField] float Speed = 1.0f;

    void Update()
    {
        transform.position = (new Vector3(Mathf.Sin(Time.time * Speed) * 15.0f, transform.position.y, transform.position.z));
    }
}
