using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Refactored by Benjamin Bartlett 5-10-22
/// </summary>
public class WallMovement : MonoBehaviour
{
    float fHeight = 20.0f;

    public GameObject[] FrontWalls;
    public GameObject[] MidWalls;
    public GameObject[] BackWalls;

    public float MidSpeed = 1.0f;
    public float BackSpeed = 1.5f;

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y <= -15.0f)
        {
            ParalaxWalls(MidWalls, MidSpeed);
            ParalaxWalls(BackWalls, BackSpeed);
        }
        RepositionWalls(FrontWalls);
        RepositionWalls(MidWalls);
        RepositionWalls(BackWalls);

    }

    void RepositionWalls(GameObject[] walls)
    {
        foreach (GameObject wall in walls)
        {
            if (transform.position.y <= (wall.transform.position.y - (fHeight / 2)))
            {
                wall.transform.Translate(0.0f, -fHeight, 0.0f); // Move wall to new position
            }
        }
    }

    void ParalaxWalls(GameObject[] walls, float speed)
    {
        foreach (GameObject wall in walls)
        {
            float moveUp = wall.transform.position.y + (speed * Time.deltaTime);
            float z = wall.transform.position.z;
            wall.transform.position = new Vector3(0.0f, moveUp, z);
        }
    }
}
