using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockingBoat : MonoBehaviour
{
    [SerializeField] float rockspeed;
    Vector3 StartPosition;
    void Start()
    {
        StartPosition = transform.position;
    }
    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.Euler(Mathf.Sin(Time.time * rockspeed) * 2.0f, -120.9f, Mathf.Cos(Time.time * rockspeed) * 3.0f);
        transform.position = new Vector3(transform.position.x, StartPosition.y + Mathf.Cos(Time.time * rockspeed) * 0.1f, transform.position.z);
    }
}
