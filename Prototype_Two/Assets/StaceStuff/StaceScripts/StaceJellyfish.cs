using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaceJellyfish : MonoBehaviour
{
    public float fTimer = 0.0f;

    bool bMoving = true;

    public GameObject OxygenTank;

    // Start is called before the first frame update
    void Start()
    {
        fTimer = Random.Range(0.0f, 0.5f);

        OxygenTank = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        fTimer = fTimer + 1.0f * Time.deltaTime;
        
        if (fTimer >= 1.0f)
        {
            fTimer = Random.Range(0.0f, 0.5f);
            bMoving = !bMoving;
        }

        if (bMoving)
        {
            transform.Translate(0.0f, 0.0f, 0.0f);
        }
        else
        {
            transform.Translate(0.0f, -OxygenTank.GetComponent<StaceOxygenCameraMovement>().fSpeed * Time.deltaTime, 0.0f);
        }

    }
}
