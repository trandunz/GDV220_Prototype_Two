using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallMovement : MonoBehaviour
{
    float fHeight = 20.0f;

    public GameObject Wall1;
    public GameObject Wall2;
    public GameObject Wall3;
    public GameObject Wall4;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // Wall 1 teleporting
        if (transform.position.y <= (Wall1.transform.position.y - (fHeight/2)))
        {
            //float fCurrentPositionY = Wall1.transform.position.y; // Get current position

            //float fNewPositionY = fCurrentPositionY - (fHeight * 2); // Calculate new position based on wall height

            Wall1.transform.Translate(0.0f, -fHeight, 0.0f); // Move wall to new position
        }

        // Wall 2 teleporting
        if (transform.position.y <= (Wall2.transform.position.y - (fHeight/2)))
        {
            //float fCurrentPositionY = Wall2.transform.position.y;

            //float fNewPositionY = fCurrentPositionY - (fWallHeight) * 2;

            Wall2.transform.Translate(0.0f, -fHeight, 0.0f);
        }

        // Wall 3 teleporting
        if (transform.position.y <= (Wall3.transform.position.y - (fHeight / 2)))
        {
            //float fCurrentPositionY = Wall2.transform.position.y;

            //float fNewPositionY = fCurrentPositionY - (fWallHeight) * 2;

            Wall3.transform.Translate(0.0f, -fHeight, 0.0f);
        }

        // Wall 2 teleporting
        if (transform.position.y <= (Wall4.transform.position.y - (fHeight / 2)))
        {
            //float fCurrentPositionY = Wall2.transform.position.y;

            //float fNewPositionY = fCurrentPositionY - (fWallHeight) * 2;

            Wall4.transform.Translate(0.0f, -fHeight, 0.0f);
        }
    }
}
