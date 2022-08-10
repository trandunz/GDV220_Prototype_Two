using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bubble : MonoBehaviour
{
    [SerializeField] float FloatSpeed;

    // Update is called once per frame
    void Update()
    {
        transform.position += Time.deltaTime * new Vector3(Mathf.Cos(Time.time * FloatSpeed * 10), FloatSpeed, 0.0f);
    }
}
