using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sink : MonoBehaviour
{

    public float sinkSpeed = 0.5f;
    public bool ison = true;

    // Update is called once per frame
    void Update()
    {
        if (ison)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - (sinkSpeed * Time.deltaTime), transform.position.z);
        }
    }
}
