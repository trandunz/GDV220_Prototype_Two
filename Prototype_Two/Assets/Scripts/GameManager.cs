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

    [Header("GameObjects")]
    public GameObject sceneCentreObject;
    public GameObject Player1;
    public GameObject Player2;
    public GameObject oxygenTankUI;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // Turn player lights on at specified depth
        if (sceneCentreObject.transform.position.y <= depthPlayerLightsOn)
        {
            Transform player1Light = Player1.transform.Find("SpotLight");
            Transform player2Light = Player2.transform.Find("SpotLight");

            player1Light.gameObject.SetActive(true);
            player2Light.gameObject.SetActive(true);
        }

        if (godMode)
        {
            oxygenTankUI.GetComponent<OxygenTankValue>().GodMode();
        }
    }
}
