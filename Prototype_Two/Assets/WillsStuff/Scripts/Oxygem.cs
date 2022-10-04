using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oxygem : MonoBehaviour
{
    private bool isExplodedGem;
    private Vector3 targetPosition;
    public float explodeSpeed;

    private void Start()
    {
        isExplodedGem = false;
    }
    private void Update()
    {
        transform.Rotate(new Vector3(0.0f, 40.0f * Time.deltaTime, 0.0f), Space.Self);
        
        if (isExplodedGem)
        {
            MoveToTarget();
        }
    }

    public void ExplodeAwayFromTank(Vector3 targetPos)
    {
        isExplodedGem = true;
        targetPosition = targetPos;
    }
    
    public void MoveToTarget()
    {
        float step = explodeSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, step);
    }
}
