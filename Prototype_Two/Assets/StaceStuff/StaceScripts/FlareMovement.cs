using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlareMovement : MonoBehaviour
{
    public float FlareYSpeed = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(0, -FlareYSpeed * Time.deltaTime, 0);
    }
}
