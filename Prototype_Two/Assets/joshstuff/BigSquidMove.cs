using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigSquidMove : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    [SerializeField] Transform target;
    // Start is called before the first frame update
    void Start()
    {
        moveSpeed = Random.Range(1.0f, 3.0f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
    }
}
