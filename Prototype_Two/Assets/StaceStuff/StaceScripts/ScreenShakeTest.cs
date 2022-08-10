using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShakeTest : MonoBehaviour
{
    private Shake shake;

    // Start is called before the first frame update
    void Start()
    {
        shake = GameObject.FindGameObjectWithTag("ScreenShake").GetComponent<Shake>();
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.M))
        {
            shake.CamShake();
            Debug.Log("Test Shaker button");
        }
    }
}
