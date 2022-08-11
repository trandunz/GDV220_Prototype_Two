using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{

    public float fCameraSpeed = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y > -200.0f)
            transform.Translate(0, -fCameraSpeed * Time.deltaTime, 0);
    }
}
