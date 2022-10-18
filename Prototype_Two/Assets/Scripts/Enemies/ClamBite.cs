using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClamBite : MonoBehaviour
{
    public GameObject audioClamBite;
    public void PlayClamBite()
    {
        Destroy(Instantiate(audioClamBite), 2.0f);
    }
}
