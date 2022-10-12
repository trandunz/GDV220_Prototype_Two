using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigFishMove : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    [SerializeField] float minSpeed = 0.6f;
    [SerializeField] float maxSpeed = 2.3f;
    [SerializeField] Transform target;

    // Start is called before the first frame update
    void Start()
    {
        moveSpeed = Random.Range(minSpeed, maxSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
    }
}
