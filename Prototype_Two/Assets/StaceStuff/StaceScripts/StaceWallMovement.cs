using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaceWallMovement : MonoBehaviour
{
    float fTimer = 0;

    public GameObject Wall1;
    public GameObject Wall2;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // Wall 1 teleporting
        if (transform.position.y <= (Wall1.transform.position.y - 20.0f))
        {
            float fPreviousPositionY = Wall1.transform.position.y;
            Debug.Log("Wall 1 Previous Y Position: " + fPreviousPositionY);

            float fNewPositionY = fPreviousPositionY - 40;

            Wall1.transform.Translate(0.0f, -40.0f, 0.0f);
            Debug.Log("Wall 1 New Y Position: " + fNewPositionY);
        }

        // Wall 2 teleporting
        if (transform.position.y <= (Wall2.transform.position.y - 20.0f))
        {
            float fPreviousPositionY = Wall2.transform.position.y;
            Debug.Log("Wall 2 Previous Y Position: " + fPreviousPositionY);

            float fNewPositionY = fPreviousPositionY - 40;

            Wall2.transform.Translate(0.0f, -40.0f, 0.0f);
            Debug.Log("Wall 2 New Y Position: " + fNewPositionY);
        }
    }
}
