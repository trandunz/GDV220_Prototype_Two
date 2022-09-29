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

    // Start is called before the first frame update
    void Start()
    {
        timer = maxTimer;

        manager = GameObject.FindGameObjectWithTag("GameManager");
        darknessStartingDepth = manager.GetComponent<GameManager>().darknessStartingDepth;
        maxTimer = manager.GetComponent<GameManager>().darknessChangeSpeed;
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

            if (timer <= 1.0f)
            {
                if (lightingLevel > 0)
                {
                    lightingLevel -= 1;
                    RenderSettings.ambientLight = new Color(lightingLevel / 255.0f, lightingLevel / 255.0f, lightingLevel / 255.0f);
                    timer = maxTimer;
                }
            }
        }
    }
}
