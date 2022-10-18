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
    private Color backgroundColor;
    private float colorG;
    private float colorB;
    private float backgroundStartDepth;
    public float timerBackground;
    private float maxTimerBackground;

    // Foreground Objects
    public GameObject[] foregroundObjects;

    // Fog
    private Color previousColor;

    // Fog colors
    private float FogTimer;
    private Color greenFogColor = new Color(0, 255, 0);
    private bool greenFogOn = false;
    private Color redFogColor = new Color(255, 0, 0);
    private bool redFogOn = false;
    private Color pinkFogColor = new Color(255, 0, 255);
    private bool pinkFogOn = false;

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
        backgroundStartDepth = manager.GetComponent<GameManager>().colorChangeStartDepth;
        maxTimerBackground = manager.GetComponent<GameManager>().colorChangeSpeed;
        timerBackground = maxTimerBackground;

        // Foreground Objects
        foregroundObjects = GameObject.FindGameObjectsWithTag("ForegroundObject");

        previousColor = RenderSettings.fogColor;

        // Set fog time
        FogTimer = manager.GetComponent<GameManager>().fogColorSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(0, -fCameraSpeed * Time.deltaTime, 0);

        // Starting darkness
        if (transform.position.y <= darknessStartingDepth && transform.position.y >= darknessStartingDepth - 5.0f)
        {
            //if (greenFogOn == false || redFogOn == false || pinkFogOn == false)
            //{
                gettingDarker = true;
            //}
        }

        // Getting darker
        if (gettingDarker == true)
        {
            timer = timer - 1.0f * Time.deltaTime; // Decrease timer by 1 second

            if (timer <= 0.0f)
            {
                if (lightingLevel > 0)
                {
                    // Decrease light
                    lightingLevel -= 1;
                    RenderSettings.ambientLight = new Color(lightingLevel / 255.0f, lightingLevel / 255.0f, lightingLevel / 255.0f);
                    timer = maxTimer;

                    // Decrease color of the fog
                    if (greenFogOn == false || redFogOn == false || pinkFogOn == false)
                    {
                        if (previousColor.r > 0)
                            previousColor.r = previousColor.r - (3.0f / 255.0f);
                        else
                            previousColor.r = 0;
                        if (previousColor.g > 0)
                            previousColor.g -= 3.0f / 255.0f;
                        else
                            previousColor.r = 0;
                        if (previousColor.b > 0)
                            previousColor.b -= 3.0f / 255.0f;
                        else
                            previousColor.r = 0;
                    }

                    RenderSettings.fogColor = new Color(previousColor.r, previousColor.g, previousColor.b);


                    // Foreground Objects - make more transparent over time when game gets darker
                    foreach (GameObject foregroundObject in foregroundObjects)
                    {
                        float alphaValue = foregroundObject.GetComponent<SpriteRenderer>().color.a;

                        if (alphaValue > 0)
                            alphaValue = alphaValue - 3.0f / 255;
                        else
                            alphaValue = 0;

                        foregroundObject.GetComponent<SpriteRenderer>().color = new Color(
                            foregroundObject.GetComponent<SpriteRenderer>().color.r,
                            foregroundObject.GetComponent<SpriteRenderer>().color.g,
                            foregroundObject.GetComponent<SpriteRenderer>().color.b,
                            alphaValue);
                    }
                }
                else
                {
                    gettingDarker = false;
                }
            }
        }

        // Turn off fog if dark
        //if (lightingLevel <= 0)
        //{
        //    RenderSettings.fog = false;
        //}

        // Fog color
        //Green
        if (transform.position.y <= manager.GetComponent<GameManager>().greenStartDepth)
        {
            greenFogOn = true;
        }
        if (transform.position.y <= manager.GetComponent<GameManager>().greenEndDepth)
        {
            greenFogOn = false;
        }
        
        if (greenFogOn == true)
        {
            FogTimer -= Time.deltaTime;
            
            if (FogTimer <= 0)
            {
                if (RenderSettings.fogColor.g < 1.0f)
                {
                    previousColor.g = previousColor.g + (5.0f / 255.0f);
                    RenderSettings.fogColor = new Color(0, previousColor.g, 0);

                    FogTimer = manager.GetComponent<GameManager>().fogColorSpeed;
                }
            }
        }

        // Red
        if (transform.position.y <= manager.GetComponent<GameManager>().redStartDepth)
        {
            redFogOn = true;
        }
        if (transform.position.y <= manager.GetComponent<GameManager>().redEndDepth)
        {
            redFogOn = false;
        }

        if (redFogOn == true)
        {
            FogTimer -= Time.deltaTime;

            if (FogTimer <= 0)
            {
                if (RenderSettings.fogColor.r < 1.0f)
                {
                    previousColor.r = previousColor.r + (5.0f / 255.0f);
                    RenderSettings.fogColor = new Color(previousColor.r, 0, 0);

                    FogTimer = manager.GetComponent<GameManager>().fogColorSpeed;
                }
            }
        }

        // Pink
        if (transform.position.y <= manager.GetComponent<GameManager>().pinkStartDepth)
        {
            pinkFogOn = true;
        }
        if (transform.position.y <= manager.GetComponent<GameManager>().pinkEndDepth)
        {
            pinkFogOn = false;
        }

        if (pinkFogOn == true)
        {
            FogTimer -= Time.deltaTime;

            if (FogTimer <= 0)
            {
                if (RenderSettings.fogColor.r < 1.0f)
                {
                    previousColor.r = previousColor.r + (5.0f / 255.0f);
                }
                if (RenderSettings.fogColor.b < 1.0f)
                {
                    previousColor.b = previousColor.b + (5.0f / 255.0f);
                }
                RenderSettings.fogColor = new Color(previousColor.r, 0, previousColor.b);

                FogTimer = manager.GetComponent<GameManager>().fogColorSpeed;
            }
        }

        // Go back to dark
        if (greenFogOn == false && redFogOn == false && pinkFogOn == false && transform.position.y <= manager.GetComponent<GameManager>().greenStartDepth)
        {
            FogTimer -= Time.deltaTime;

            if (FogTimer <= 0)
            {
                if (RenderSettings.fogColor.r > 0.0f)
                {
                    previousColor.r = previousColor.r - (5.0f / 255.0f);
                }
                if (RenderSettings.fogColor.g > 0.0f)
                {
                    previousColor.g = previousColor.g - (5.0f / 255.0f);
                }
                if (RenderSettings.fogColor.b > 0.0f)
                {
                    previousColor.b = previousColor.b - (5.0f / 255.0f);
                }
                RenderSettings.fogColor = new Color(previousColor.r, previousColor.g, previousColor.b);
                FogTimer = manager.GetComponent<GameManager>().fogColorSpeed;
            }
        }


        if (transform.position.y <= backgroundStartDepth)
        {
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
}
