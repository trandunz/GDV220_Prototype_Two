using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IKInitialiser : MonoBehaviour
{
    public float length;
    public FastIKFabric rightPlayer;
    public FastIKFabric leftPlayer;
    public GameObject Sphere;

    public bool moveleft = false;
    public bool moveright = false;
    // Start is called before the first frame update
    void Awake()
    {
        for (int i = 0; i < length + (1 * PlayerPrefs.GetInt("TetherUpgrade Level")); i++)
        {
            rightPlayer.AttachNewSphere(Instantiate(Sphere), 0.5f);
            leftPlayer.AttachNewSphere(Instantiate(Sphere), -0.5f);
        }
    }

    public IEnumerator RemoveRight()
    {
        yield return new WaitForEndOfFrame();
        if (rightPlayer.Bones.Length > 9)
        {
            rightPlayer.RemomveSphere(0.5f);
            leftPlayer.AttachNewSphere(Instantiate(Sphere), -0.5f);
        }
        moveleft = false;
    }

    public IEnumerator RemoveLeft()
    {
        yield return new WaitForEndOfFrame();
        if (leftPlayer.Bones.Length > 9)
        {
            leftPlayer.RemomveSphere(-0.5f);
            rightPlayer.AttachNewSphere(Instantiate(Sphere), 0.5f);
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
