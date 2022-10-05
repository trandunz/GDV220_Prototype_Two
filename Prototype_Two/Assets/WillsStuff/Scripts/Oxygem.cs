using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oxygem : MonoBehaviour
{
    public bool isExplodedGem;
    private Vector3 targetPosition;
    public float explodeSpeed;
    private float randomSpinSpeed;
    public float lifeTime = 2.0f;

    private void Update()
    {
        float spin = 40.0f * Time.deltaTime;
        transform.Rotate(new Vector3(0.0f, spin, 0.0f), Space.Self);
        
        if (isExplodedGem)
        {
            spin *= randomSpinSpeed;
            transform.Rotate(new Vector3(spin, spin, spin), Space.Self);
            MoveToTarget();

            lifeTime -= Time.deltaTime;
            if (lifeTime <= 0.0f)
            {
                Destroy(gameObject);
            }
        }
    }

    public void ExplodeAwayFromTank(Vector3 targetPos)
    {
        isExplodedGem = true;
        targetPosition = targetPos;
        Debug.Log("Exploded");
        randomSpinSpeed = Random.Range(2.0f, 10.0f);
    }
    
    public void MoveToTarget()
    {
        float step = explodeSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, step);

    }
}
