using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oxygem : MonoBehaviour
{
    public bool isExplodedGem;
    private Vector3 targetPosition;
    private Vector3 targetScale;
    private float randomSpinSpeed;
    public float lifeTime = 2.0f;
    float startLifetime;
    Vector3 StartScale;
    Vector3 StartPos;

    private void Start()
    {
        startLifetime = lifeTime;
        StartScale = transform.localScale;
        StartPos = transform.position;
    }
    private void Update()
    {
        float spin = 40.0f * Time.deltaTime;
        transform.Rotate(new Vector3(0.0f, spin, 0.0f), Space.Self);
        
        if (isExplodedGem)
        {
            spin *= randomSpinSpeed;
            transform.Rotate(new Vector3(spin, spin, spin), Space.Self);

            transform.localScale = Vector3.Lerp(targetScale, StartScale, lifeTime / startLifetime);
            transform.position = Vector3.Lerp(targetPosition, StartPos, lifeTime / startLifetime);

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
        targetPos.z = -10.0f;
        targetPosition = targetPos;
        targetScale = Vector3.one * 9.0f;
        Debug.Log("Exploded");
        randomSpinSpeed = Random.Range(2.0f, 10.0f);
    }
   
}
