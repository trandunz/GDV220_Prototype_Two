using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IKInitialiser : MonoBehaviour
{
    public float length;
    public FastIKFabric rightPlayer;
    public FastIKFabric leftPlayer;
    public GameObject Sphere;
    // Start is called before the first frame update
    void Awake()
    {
        for (int i = 0; i < length; i++)
        {
            rightPlayer.AttachNewSphere(Instantiate(Sphere), 0.5f);
            leftPlayer.AttachNewSphere(Instantiate(Sphere), -0.5f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
