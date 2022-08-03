using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaceOxygenCameraMovement : MonoBehaviour
{

    float fSpeed = 4.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(0, -fSpeed * Time.deltaTime, 0);
    }
}
