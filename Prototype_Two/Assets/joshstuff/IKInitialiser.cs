using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IKInitialiser : MonoBehaviour
{
    public float length;
    public FastIKFabric rightPlayer;
    public FastIKFabric leftPlayer;
    public GameObject Sphere;

    public int MinChainLength = 15;
    public float sphereSize = 0.3f;

    public bool moveleft = false;
    public bool moveright = false;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < length + (4 * 3); i++)
        {
            rightPlayer.AttachNewSphere(Instantiate(Sphere), sphereSize);
            leftPlayer.AttachNewSphere(Instantiate(Sphere), -sphereSize);
        }
    }

    public IEnumerator RemoveRight()
    {
        yield return new WaitForEndOfFrame();
        if (rightPlayer.Bones.Length > MinChainLength)
        {
            rightPlayer.RemomveSphere(0.5f);
            leftPlayer.AttachNewSphere(Instantiate(Sphere), -sphereSize);
        }
        moveleft = false;
    }

    public IEnumerator RemoveLeft()
    {
        yield return new WaitForEndOfFrame();
        if (leftPlayer.Bones.Length > MinChainLength)
        {
            leftPlayer.RemomveSphere(-0.5f);
            rightPlayer.AttachNewSphere(Instantiate(Sphere), sphereSize);
        }
        moveright = false;
    }

    private void Update()
    {
        if (moveright)
        {
            StartCoroutine(RemoveLeft());
        }
        if (moveleft)
        {
            StartCoroutine(RemoveRight());
        }
    }
}
