using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OxygenTankValue : MonoBehaviour
{
    public GameObject fadeToBlack; // Gameobject holding fade to black script

    // How much each level of oxygen upgrade decreases oxygen
    [SerializeField] private int iMaxOxygen;
    public float fDrainSpeed0 = 0.0f;
    public float fDrainSpeed1 = 0.0f;
    public float fDrainSpeed2 = 0.0f;
    public float fDrainSpeed3 = 0.0f;
    public float fDrainSpeed4 = 0.0f;
    public float fDrainSpeed5 = 0.0f;
    private Vector3 scaleAmountDrain;
    
    // Damage (from enemies/kina etc)
    public float fDamageAmount = 0.5f;

    // Dash efficiency
    [SerializeField] private int iMaxDash;
    public float fDashLoss0 = 0.0f;
    public float fDashLoss1 = 0.0f;
    public float fDashLoss2 = 0.0f;
    public float fDashLoss3 = 0.0f;
    public float fDashLoss4 = 0.0f;
    public float fDashLoss5 = 0.0f;
    private Vector3 dashAmountDrain;

    /*    // Shoot efficiency
        [SerializeField] private int iMaxShoot;
        public float fShootLoss0 = 0.0f;
        public float fShootLoss1 = 0.0f;
        public float fShootLoss2 = 0.0f;
        public float fShootLoss3 = 0.0f;
        public float fShootLoss4 = 0.0f;
        public float fShootLoss5 = 0.0f;
        private Vector3 shootAmountDrain;*/

    // Oxygen metre flash
    public float fFlashMinScale = 0.2f;
    private float fFlashTimer = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        // Get the upgrade levels
        iMaxOxygen = PlayerPrefs.GetInt("MaxOxygen Level"); // Get max oxygen upgrade level
        iMaxDash = PlayerPrefs.GetInt("DashUpgrade Level"); // Get dash upgrade level (efficiency)
       /* iMaxShoot = PlayerPrefs.GetInt("ShotUpgrade Level"); // Get shoot upgrade level (efficiency)*/

        // Change oxygen drain amount based on upgrade
        if (iMaxOxygen == 0)
        {
            scaleAmountDrain = new Vector3(0.0f, fDrainSpeed0, 0.0f);
        }
        else if (iMaxOxygen == 1)
        {
            scaleAmountDrain = new Vector3(0.0f, fDrainSpeed1, 0.0f);
        }
        else if (iMaxOxygen == 2)
        {
            scaleAmountDrain = new Vector3(0.0f, fDrainSpeed2, 0.0f);
        }
        else if (iMaxOxygen == 3)
        {
            scaleAmountDrain = new Vector3(0.0f, fDrainSpeed3, 0.0f);
        }
        else if (iMaxOxygen == 4)
        {
            scaleAmountDrain = new Vector3(0.0f, fDrainSpeed4, 0.0f);
        }
        else if (iMaxOxygen == 5)
        {
            scaleAmountDrain = new Vector3(0.0f, fDrainSpeed5, 0.0f);
        }

        // Change dash drain amount based on upgrade
        if (iMaxDash == 0)
        {
            dashAmountDrain = new Vector3(0.0f, fDashLoss0, 0.0f);
        }
        else if (iMaxDash == 1)
        {
            dashAmountDrain = new Vector3(0.0f, fDashLoss1, 0.0f);
        }
        else if (iMaxDash == 2)
        {
            dashAmountDrain = new Vector3(0.0f, fDashLoss2, 0.0f);
        }
        else if (iMaxDash == 3)
        {
            dashAmountDrain = new Vector3(0.0f, fDashLoss3, 0.0f);
        }
        else if (iMaxDash == 4)
        {
            dashAmountDrain = new Vector3(0.0f, fDashLoss4, 0.0f);
        }
        else if (iMaxDash == 5)
        {
            dashAmountDrain = new Vector3(0.0f, fDashLoss5, 0.0f);
        }

        // Change shoot drain amount based on upgrade
        /*if (iMaxShoot == 0)
        {
            shootAmountDrain = new Vector3(0.0f, fShootLoss0, 0.0f);
        }
        else if (iMaxShoot == 1)
        {
            shootAmountDrain = new Vector3(0.0f, fShootLoss1, 0.0f);
        }
        else if (iMaxShoot == 2)
        {
            shootAmountDrain = new Vector3(0.0f, fShootLoss2, 0.0f);
        }
        else if (iMaxShoot == 3)
        {
            shootAmountDrain = new Vector3(0.0f, fShootLoss3, 0.0f);
        }
        else if (iMaxShoot == 4)
        {
            shootAmountDrain = new Vector3(0.0f, fShootLoss4, 0.0f);
        }
        else if (iMaxShoot == 5)
        {
            shootAmountDrain = new Vector3(0.0f, fShootLoss5, 0.0f);
        }*/
    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale -= scaleAmountDrain * Time.deltaTime;

        // If oxygen is 0 (or less), then start fade to black script
        if (transform.localScale.y <= 0.0f)
        {
            scaleAmountDrain = new Vector3(0.0f, 0.0f, 0.0f);
            fadeToBlack.GetComponent<FadeToBlack>().bFading = true;
        }

        // Testing - forces damage
        if (Input.GetKeyDown(KeyCode.V))
        {
            DamageOxygenUse();
        }
        // Testing - forces dash
        if (Input.GetKeyDown(KeyCode.C))
        {
            DashOxygenUse();
        }
        // Testing - forces shoot oxygen use
        /*if (Input.GetKeyDown(KeyCode.X))
        {
            ShootOxygenUse();
        }*/

        if (Input.GetKeyDown(KeyCode.J))
        {
            gameObject.GetComponent<Flash>().FlashStart();
        }

        if (transform.localScale.y <= fFlashMinScale)
        {
            fFlashTimer = fFlashTimer + 1 * Time.deltaTime;
            if (fFlashTimer >= 1)
            {
                Debug.Log("Test");
                gameObject.GetComponent<Flash>().FlashStart();
                fFlashTimer = 0;
            }
        }
    }

    // Losing oxygen when damaged
    public void DamageOxygenUse()
    {
        if (transform.localScale.y > fDamageAmount)
        {
            transform.localScale -= new Vector3(0.0f, fDamageAmount, 0.0f);
        }
        else
        {
            transform.localScale = new Vector3(0.0f, 0.0f, 0.0f);
        }
    }

    // Losing oxygen when dashing
    public void DashOxygenUse()
    {
        if (transform.localScale.y > dashAmountDrain.y)
        {
            transform.localScale -= dashAmountDrain;
        }
        else
        {
            transform.localScale = new Vector3(0.0f, 0.0f, 0.0f);
        }
    }

    // Losing oxygen when shooting
    /*public void ShootOxygenUse()
    {
        if (transform.localScale.y > shootAmountDrain.y)
        {
            transform.localScale -= shootAmountDrain;
        }
        else
        {
            transform.localScale = new Vector3(0.0f, 0.0f, 0.0f);
        }
    }*/

    public void AddOxygen(float _amount)
    {
        transform.localScale += new Vector3(0.0f, _amount, 0.0f);
        Vector3.ClampMagnitude(transform.localScale, 1);
    }
}
