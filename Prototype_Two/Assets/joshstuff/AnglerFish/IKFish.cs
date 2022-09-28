using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IKFish : MonoBehaviour
{
    public float length;
    public FishIKFabrik Gem;
    public GameObject Sphere;

    public float sphereSize = 0.3f;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < length + (4 * 3); i++)
        {
            Gem.AttachNewSphere(Instantiate(Sphere), sphereSize);
        }
    }
}
