using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OxygenTankValue : MonoBehaviour
{
    // How much each level of oxygen upgrade decreases oxygen
    public float fDrainSpeed0 = 0.0f;
    public float fDrainSpeed1 = 0.0f;
    public float fDrainSpeed2 = 0.0f;
    public float fDrainSpeed3 = 0.0f;
    public float fDrainSpeed4 = 0.0f;
    public float fDrainSpeed5 = 0.0f;

    Vector3 scaleAmount;
    [SerializeField] private int iMaxOxygen;

    // Start is called before the first frame update
    void Start()
    {
        iMaxOxygen = PlayerPrefs.GetInt("MaxOxygen");

        if (iMaxOxygen == 0)
        {
            scaleAmount = new Vector3(0.0f, fDrainSpeed0, 0.0f);
        }
        else if (iMaxOxygen == 1)
        {
            scaleAmount = new Vector3(0.0f, fDrainSpeed1, 0.0f);
        }
        else if (iMaxOxygen == 2)
        {
            scaleAmount = new Vector3(0.0f, fDrainSpeed2, 0.0f);
        }
        else if (iMaxOxygen == 3)
        {
            scaleAmount = new Vector3(0.0f, fDrainSpeed3, 0.0f);
        }
        else if (iMaxOxygen == 4)
        {
            scaleAmount = new Vector3(0.0f, fDrainSpeed4, 0.0f);
        }
        else if (iMaxOxygen == 5)
        {
            scaleAmount = new Vector3(0.0f, fDrainSpeed5, 0.0f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale -= scaleAmount * Time.deltaTime;
    }
}
