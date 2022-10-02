using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float fCameraSpeed = 1.0f;

    // Screen lighting
    [Header("DarknessOverTime")]
    private GameObject manager;
    private float darknessStartingDepth;
    public bool gettingDarker = false;

    public int lightingLevel = 100;
    public float timer = 0.0f;
    private float maxTimer;

    // Background image - changing to darker blue over time
    [Header("BackgroundColorChangeOverTime")]
    public GameObject backgroundObject;
    public Color backgroundColor;
    public float colorG;
    public float colorB;
    public float timerBackground;
    public float maxTimerBackground = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.FindGameObjectWithTag("GameManager");
        darknessStartingDepth = manager.GetComponent<GameManager>().darknessStartingDepth;
        maxTimer = manager.GetComponent<GameManager>().darknessChangeSpeed;

        // Screen lighting
        timer = maxTimer;

        // Background image 
        colorG = backgroundObject.GetComponent<MeshRenderer>().material.color.g;
        colorB = backgroundObject.GetComponent<MeshRenderer>().material.color.b;
        timerBackground = maxTimerBackground;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(0, -fCameraSpeed * Time.deltaTime, 0);

        // Starting darkness
        if (transform.position.y <= darknessStartingDepth)
        {
            gettingDarker = true;
        }

        // Getting darker
        if (gettingDarker == true)
        {
            timer = timer - 1.0f * Time.deltaTime; // Decrease timer by 1 second

            if (timer <= 0.0f)
            {
                if (lightingLevel > 0)
                {
                    lightingLevel -= 1;
                    RenderSettings.ambientLight = new Color(lightingLevel / 255.0f, lightingLevel / 255.0f, lightingLevel / 255.0f);
                    timer = maxTimer;
                }
            }
        }

        // Background image - make darker over time
        timerBackground = timerBackground - 1.0f * Time.deltaTime; // Decrease timer by 1 second
        if (timerBackground <= 0.0f)
        {
            // Change color of background mat
            if (colorG >= 53.0f / 255.0f)
            {
                colorG -= 1.0f / 255.0f;
            }
            if (colorB >= 94.0f / 255.0f)
            {
                colorB -= 1.0f / 255.0f;
            }
            backgroundObject.GetComponent<MeshRenderer>().material.color = new Color(0, colorG, colorB);
            timerBackground = maxTimerBackground; // reset timer
        }
    }
}
