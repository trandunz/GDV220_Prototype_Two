using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flare : MonoBehaviour
{
    public float FlareYSpeed = 1.0f;


    // Materials
    public Material greenMat;
    public Material magentaMat;
    public Material orangeMat;
    public Material redMat;

    public MeshRenderer flareObjectMeshRenderer;

    // Start is called before the first frame update
    void Start()
    {
        int iRandomNum = Random.Range(1, 5);
        if (iRandomNum == 1)
        {
            flareObjectMeshRenderer.material = greenMat;
        }
        else if (iRandomNum == 2)
        {
            flareObjectMeshRenderer.material = magentaMat;
        }
        else if (iRandomNum == 3)
        {
            flareObjectMeshRenderer.material = orangeMat;
        }
        else
        {
            flareObjectMeshRenderer.material = redMat;
        }
        transform.GetComponentInChildren<Light>().color = flareObjectMeshRenderer.material.color;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(0, -FlareYSpeed * Time.deltaTime, 0);
    }
}
