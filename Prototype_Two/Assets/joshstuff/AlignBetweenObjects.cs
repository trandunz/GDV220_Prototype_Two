using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlignBetweenObjects : MonoBehaviour
{
    [SerializeField] GameObject object1;
    [SerializeField] GameObject object2;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(object1.transform);
    }
}
