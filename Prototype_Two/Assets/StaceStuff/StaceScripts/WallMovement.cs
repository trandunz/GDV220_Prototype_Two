using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallMovement : MonoBehaviour
{
    public float fWallHeight = 20.0f;

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
        if (transform.position.y <= (Wall1.transform.position.y - fWallHeight))
        {
            float fCurrentPositionY = Wall1.transform.position.y; // Get current position

            float fNewPositionY = fCurrentPositionY - (fWallHeight * 2); // Calculate new position based on wall height

            Wall1.transform.Translate(0.0f, -(fWallHeight * 2), 0.0f); // Move wall to new position
        }

        // Wall 2 teleporting
        if (transform.position.y <= (Wall2.transform.position.y - fWallHeight))
        {
            float fCurrentPositionY = Wall2.transform.position.y;

            float fNewPositionY = fCurrentPositionY - (fWallHeight) * 2;

            Wall2.transform.Translate(0.0f, -(fWallHeight * 2), 0.0f);
        }
    }
}
