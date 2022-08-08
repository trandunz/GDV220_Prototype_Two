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

    public GameObject fadeToBlack;

    public bool bDamaged = false;
    public float fDamageAmount = 0.5f;

    public bool bDashed = false;
    public float fDashAmount = 0.25f;

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

        // If oxygen is 0 (or less), then start fade to black script
        if (transform.localScale.y <= 0.0f)
        {
            scaleAmount = new Vector3(0.0f, 0.0f, 0.0f);
            fadeToBlack.GetComponent<FadeToBlack>().bFading = true;
        }

        // Losing oxygen when damaged
        if (bDamaged == true)
        {
            if (transform.localScale.y > fDamageAmount)
            {
                transform.localScale -= new Vector3(0.0f, fDamageAmount, 0.0f);
            }
            else
            {
                transform.localScale = new Vector3(0.0f, 0.0f, 0.0f);
            }
            bDamaged = false;
        }

        if (bDashed == true)
        {
            if (transform.localScale.y > fDashAmount)
            {
                transform.localScale -= new Vector3(0.0f, fDashAmount, 0.0f);
            }
            else
            {
                transform.localScale = new Vector3(0.0f, 0.0f, 0.0f);
            }
            bDashed = false;
        }

        // Testing - forces fade to black
        if (Input.GetKeyDown(KeyCode.V))
        {
            bDamaged = true;
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            bDashed = true;
        }
    }
}
