using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EelMovingRightToLeft : MonoBehaviour
{
    GameObject OxygenTank;
    public float fMoveSpeed = 2.0f;
    public float fDist = 5.0f;

    bool bMoving = false;

    public float timeToMove = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        OxygenTank = GameObject.FindGameObjectWithTag("SceneCentre");
    }

    // Update is called once per frame
    void Update()
    {
        timeToMove -= 1 * Time.deltaTime;

        //if (OxygenTank.transform.position.y - transform.position.y <= fDist)
        if (timeToMove <= 0) 
        {
            bMoving = true;
        }

        if (bMoving == true)
        {
            transform.Translate(-fMoveSpeed * Time.deltaTime, 0.0f, 0.0f);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag is "Player")
        {
            other.GetComponent<SwimController>().HitEnemy();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag is "Player")
        {
            other.GetComponent<SwimController>().StartInvulnrability();
        }
    }
}
