using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Jellyfish : MonoBehaviour
{
    GameObject OxygenTank;
    public float fTimer = 0.0f;
    public float fMinRange = 0.0f;
    public float fMaxRange = 0.5f;

    bool bMoving = true;

    // Start is called before the first frame update
    void Start()
    {
        fTimer = Random.Range(fMinRange, fMaxRange);

        OxygenTank = GameObject.FindGameObjectWithTag("SceneCentre");
    }

    // Update is called once per frame
    void Update()
    {
        fTimer = fTimer + 1.0f * Time.deltaTime; // Increase timer by 1 second
        
        if (fTimer >= 1.0f)
        {
            fTimer = Random.Range(fMinRange, fMaxRange);
            bMoving = !bMoving;
        }

        if (bMoving)
        {
            transform.Translate(0.0f, 0.0f, 0.0f); // Stop movement
        }
        else
        {
            // Movement follows camera speed - makes jellyfish look like they stop moving
            transform.Translate(0.0f, OxygenTank.GetComponentInParent<CameraMovement>().fCameraSpeed * Time.deltaTime, 0.0f); 
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag is "Player")
        {
            other.GetComponent<SwimController>().HitEnemy();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag is "Player")
        {
            other.GetComponent<SwimController>().LeaveEnemy();
        }
    }

}
