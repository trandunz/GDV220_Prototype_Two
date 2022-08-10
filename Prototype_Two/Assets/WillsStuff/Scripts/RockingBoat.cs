using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockingBoat : MonoBehaviour
{
    [SerializeField] float rockspeed;
    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.Euler(Mathf.Sin(Time.time * rockspeed) * 2.0f, -90, Mathf.Cos(Time.time * rockspeed) * 3.0f);
        transform.position = new Vector3(transform.position.x, Mathf.Cos(Time.time * rockspeed) * 0.1f, transform.position.z);
    }
}
