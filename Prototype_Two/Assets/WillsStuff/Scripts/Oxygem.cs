using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oxygem : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag is "Player")
        {
            GemManager.instance.AddGems(1);
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        transform.Rotate(new Vector3(0.0f, 40.0f * Time.deltaTime, 0.0f), Space.Self);
    }
}
