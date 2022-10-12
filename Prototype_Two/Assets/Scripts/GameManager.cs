using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Godmode")]
    public bool godMode;

    [Header("DarknessOverTime")]
    public float darknessStartingDepth = -50.0f; // Minimum depth before scene starts going dark
    public float darknessChangeSpeed = 2.0f; // Amount of time before a change in scene color towards black
                                             // over a total time of 100 seconds
    public float depthPlayerLightsOn = -100.0f; // At what depth the player spotlights should turn on
    public float playerLightIntensityIncrease = 1;
    public float PlayerLightTimer = 2.0f;
    private float LightTimer;
    private Transform player1Light;
    private Transform player2Light;
    private float PlayerLightIntensity;

    // Background object and its changing of color over time
    [Header("Background Object")]
    public float colorChangeStartDepth = -20.0f;
    public float colorChangeSpeed = 2.0f;

    [Header("GameObjects")]
    public GameObject sceneCentreObject;
    public GameObject Player1;
    public GameObject Player2;
    public GameObject oxygenTankUI;

    // Start is called before the first frame update
    void Start()
    {
        LightTimer = PlayerLightTimer;

        // Find player lights
        player1Light = Player1.transform.Find("SpotLight");
        player2Light = Player2.transform.Find("SpotLight");

        PlayerLightIntensity = player1Light.GetComponent<Light>().intensity;
        player1Light.GetComponent<Light>().intensity = 0;
        player2Light.GetComponent<Light>().intensity = 0;
    }

    // Update is called once per frame
    void Update()
    {
        // Turn player lights on at specified depth
        if (sceneCentreObject.transform.position.y <= depthPlayerLightsOn)
        {
            LightTimer -= Time.deltaTime;
            if (LightTimer <= 0)
            {
                LightTimer = PlayerLightTimer; // Reset timer

                if (player1Light.GetComponent<Light>().intensity < PlayerLightIntensity)
                {
                    player1Light.GetComponent<Light>().intensity = player1Light.GetComponent<Light>().intensity + playerLightIntensityIncrease;
                    player2Light.GetComponent<Light>().intensity = player1Light.GetComponent<Light>().intensity + playerLightIntensityIncrease;
                }
            }


            //Transform player1Light = Player1.transform.Find("SpotLight");
            //Transform player2Light = Player2.transform.Find("SpotLight");

            //player1Light.gameObject.SetActive(true);
            //player2Light.gameObject.SetActive(true);
        }

        if (godMode)
        {
            oxygenTankUI.GetComponent<OxygenTankValue>().GodMode();
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            for (int i = 0; i < 9; i++)
            {
                oxygenTankUI.GetComponent<OxygenTankValue>().AddOxygem();
            }

            oxygenTankUI.GetComponent<OxygenTankValue>().DamageOxygenUse();
        }
    }
}
