using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class offsetMaterial : MonoBehaviour
{

    [SerializeField] Material material;
    [SerializeField] float xSpeed = 0.2f;
    [SerializeField] float ySpeed = 0.2f;
    [SerializeField] float xOffset = 0;
    [SerializeField] float yOffset = 0;
    // Start is called before the first frame update
    void Start()
    {
        material = GetComponent<Renderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        xOffset += xSpeed * Time.deltaTime;
        if (xOffset >= 1)
        {
            xOffset -= 1;
        }
        else if (xOffset <= -1)
        {
            xOffset += 1;
        }

        yOffset += ySpeed * Time.deltaTime;
        if (yOffset >= 1)
        {
            yOffset -= 1;
        }
        else if (yOffset <= -1)
        {
            yOffset += 1;
        }

        material.SetTextureOffset("_MainTex", new Vector2(xOffset, yOffset));
    }
}
