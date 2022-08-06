using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EelMoving : MonoBehaviour
{
    GameObject OxygenTank;
    public float fMoveSpeed = 2.0f;
    public float fDist = 5.0f;

    bool bMoving = false;

    // Start is called before the first frame update
    void Start()
    {
        OxygenTank = GameObject.FindGameObjectWithTag("SceneCentre");
    }

    // Update is called once per frame
    void Update()
    {
        if (OxygenTank.transform.position.y - transform.position.y <= fDist)
        {
            bMoving = true;
        }

        if (bMoving == true)
        {
            transform.Translate(fMoveSpeed * Time.deltaTime, 0.0f, 0.0f);
        }
    }
}
