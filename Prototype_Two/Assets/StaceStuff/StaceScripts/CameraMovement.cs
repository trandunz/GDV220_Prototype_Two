using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float fCameraSpeed = 1.0f;

    // Screen lighting
    public float fDarknessStartingDepth = -50.0f;
    public bool bGettingDarker = false;

    public int iLightingLevel = 100;
    public float fTimer = 0.0f;
    public float fMaxTimer = 10.0f;



    // Start is called before the first frame update
    void Start()
    {
        fTimer = fMaxTimer;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(0, -fCameraSpeed * Time.deltaTime, 0);

        // Starting darkness
        if (transform.position.y <= fDarknessStartingDepth)
        {
            bGettingDarker = true;
        }

        // Getting darker
        if (bGettingDarker == true)
        {
            fTimer = fTimer - 1.0f * Time.deltaTime; // Decrease timer by 1 second

            if (fTimer <= 1.0f)
            {
                if (iLightingLevel > 0)
                {
                    iLightingLevel -= 1;
                    RenderSettings.ambientLight = new Color(iLightingLevel / 255.0f, iLightingLevel / 255.0f, iLightingLevel / 255.0f);
                    fTimer = fMaxTimer;
                }
            }
        }
    }
}
