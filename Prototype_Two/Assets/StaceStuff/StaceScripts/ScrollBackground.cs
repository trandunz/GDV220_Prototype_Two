using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollBackground : MonoBehaviour
{
    public float fCameraSpeed;

    public GameObject gameCentreObject;

    public float fScrollingStartPosY = -40.0f;
    public bool bScrolling = false;
    public float fYPos;

    // Start is called before the first frame update
    void Start()
    {
        fCameraSpeed = gameCentreObject.GetComponent<CameraMovement>().fCameraSpeed;
        fYPos = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        fYPos = transform.position.y;

        if (gameCentreObject.transform.position.y <= fScrollingStartPosY)
        {
            bScrolling = true;
        }

        if (bScrolling == true)
        {
            transform.Translate(0, -fCameraSpeed * Time.deltaTime, 0);
        }
    }
}
