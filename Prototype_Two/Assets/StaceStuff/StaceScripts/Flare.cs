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

    public MeshRenderer flareObjectMeshRenderer1;
    public MeshRenderer flareObjectMeshRenderer2;

    // Start is called before the first frame update
    void Start()
    {
        int iRandomNum = Random.Range(1, 5);
        if (iRandomNum == 1)
        {
            flareObjectMeshRenderer1.material = greenMat;
            flareObjectMeshRenderer2.material = greenMat;
        }
        else if (iRandomNum == 2)
        {
            flareObjectMeshRenderer1.material = magentaMat;
            flareObjectMeshRenderer2.material = magentaMat;
        }
        else if (iRandomNum == 3)
        {
            flareObjectMeshRenderer1.material = orangeMat;
            flareObjectMeshRenderer2.material = orangeMat;
        }
        else
        {
            flareObjectMeshRenderer1.material = redMat;
            flareObjectMeshRenderer2.material = redMat;
        }
        transform.GetComponentInChildren<Light>().color = flareObjectMeshRenderer1.material.color;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(0, -FlareYSpeed * Time.deltaTime, 0);
    }
}
