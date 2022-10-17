using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OxygenTankValue : MonoBehaviour
{
    [Header("Oxygem Counter")]
    [SerializeField] public int iOxygemCount;
    [SerializeField] public GameObject[] litOxygem;
    [SerializeField] public GameObject explodedOxygem;
    private GameObject[] spawnedExplodedGems;

    public GameObject fadeToBlack; // Gameobject holding fade to black script

    // How much each level of oxygen upgrade decreases oxygen
    [SerializeField] private int iMaxOxygen;
    [SerializeField] private float fBubbleSpawnPercent = 0.1f;
    public float fDrainSpeed0 = 0.0f;
    public float fDrainSpeed1 = 0.0f;
    public float fDrainSpeed2 = 0.0f;
    public float fDrainSpeed3 = 0.0f;
    public float fDrainSpeed4 = 0.0f;
    public float fDrainSpeed5 = 0.0f;
    private Vector3 scaleAmountDrain;

    // Damage (from enemies/kina etc)
    public float fDamageAmount = 0.5f;
    float m_DamageTime = 0.2f;
    float m_DamageTimer = 0.0f;

    // Dash efficiency
    [SerializeField] private int iMaxDash;
    public float fDashLoss0 = 0.0f;
    public float fDashLoss1 = 0.0f;
    public float fDashLoss2 = 0.0f;
    public float fDashLoss3 = 0.0f;
    public float fDashLoss4 = 0.0f;
    public float fDashLoss5 = 0.0f;
    private Vector3 dashAmountDrain;

    // Oxygen metre flash
    public float fFlashMinScale = 0.2f;
    private float fFlashTimer = 0.0f;

    [Header("Audio")]
    public GameObject audioDrown;
    private bool bHasPlayedDrownSound;

    SpawnManager spawnManager;

    // Start is called before the first frame update
    void Start()
    {
        bHasPlayedDrownSound = false;

        // Get the upgrade levels
        iMaxOxygen = 1; // Get max oxygen upgrade level
        iMaxDash = 3; // Get dash upgrade level (efficiency)
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

        spawnManager = FindObjectOfType<SpawnManager>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale -= scaleAmountDrain * Time.deltaTime;

        // If oxygen is 0 (or less), then start fade to black script
        if (transform.localScale.y <= 0.0f)
        {
            Drown();
            scaleAmountDrain = new Vector3(0.0f, 0.0f, 0.0f);
            fadeToBlack.GetComponent<FadeToBlack>().bFading = true;
        }

        //if (Input.GetKeyDown(KeyCode.J))
        //{
        //    gameObject.GetComponent<Flash>().FlashStart();
        //}

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

        if (transform.localScale.y <= fBubbleSpawnPercent)
        {
            spawnManager.GetComponent<SpawnManager>().SpawnBubble();
        }

        if (m_DamageTimer > 0.0f)
        {
            m_DamageTimer -= Time.deltaTime;
        }
    }

    // Losing oxygen when damaged
    public void DamageOxygenUse()
    {
        if (m_DamageTimer <= 0.0f)
        {
            m_DamageTimer = m_DamageTime;

            // First check if has gems
            if (iOxygemCount == 0)
            {
                StartCoroutine(lerpBarScaleRoutine(-fDamageAmount));
            }
            else
            {
                ExplodeGems(iOxygemCount);
                iOxygemCount = 0;
                LightGems();
            }
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
            transform.localScale = new Vector3(transform.localScale.x, 0.0f, transform.localScale.z);
        }
    }

    public void AddOxygen(float _amount)
    {
        StartCoroutine(lerpBarScaleRoutine(_amount));
    }
    IEnumerator lerpBarScaleRoutine(float _toAmount)
    {
        float ratio = 0.0f;
        Vector3 startScale = transform.localScale;
        while (ratio < 1)
        {
            transform.localScale = Vector3.Lerp(startScale, startScale + Vector3.up * _toAmount, ratio);

            if (transform.localScale.y < 0)
            {
                startScale.y = 0;
                transform.localScale = startScale;
            }
            else if (transform.localScale.y > 1)
            {
                startScale.y = 1.0f;
                transform.localScale = startScale;
            }

            ratio += Time.deltaTime* 3.0f;
            yield return new WaitForEndOfFrame();
        }

        if (transform.localScale.y < 0)
        {
            startScale.y = 0;
            transform.localScale = startScale;
        }
        else if (transform.localScale.y > 1)
        {
            startScale.y = 1.0f;
            transform.localScale = startScale;
        }
            
    }

    private void Drown()
    {
        if (!bHasPlayedDrownSound)
        {
            Destroy(Instantiate(audioDrown), 5.0f);
            bHasPlayedDrownSound = true;

            foreach(var player in FindObjectsOfType<SwimController>())
            {
                player.Die();
            }
        }
    }

    public void AddOxygem()
    {
        iOxygemCount++;
        
        if (iOxygemCount == 10)
        {
            AddOxygen(0.8f);
            iOxygemCount = 0;
        }
        LightGems();
    }

    private void LightGems()
    {
        for (int i = 0; i < 10; i++)
        {
            if (i < iOxygemCount)
            {
                litOxygem[i].SetActive(true);
            }
            else
            {
                litOxygem[i].SetActive(false);
            }
        }
    }
    public Transform GetNextOxygemSlot()
    {
        return litOxygem[iOxygemCount].transform;
    }

    private void ExplodeGems(int gemsCount)
    {
        for (int i = 0; i<gemsCount; ++i)
        {
            // Get a random point to "explode" towards
            float angle = Random.Range(0.0f, 359);
            float x = Mathf.Cos(Mathf.Deg2Rad * angle) * 20.0f;
            float y = Mathf.Sin(Mathf.Deg2Rad * angle) * 20.0f;

            x += transform.position.x;
            y += transform.position.y;
            Vector3 explodeTarget = new Vector3(x, y, transform.position.z);

            GameObject gem = Instantiate(explodedOxygem, this.transform.position, Quaternion.identity);
            gem.GetComponent<Oxygem>().ExplodeAwayFromTank(explodeTarget);
        }
        
    }

    public void GodMode()
    {
        AddOxygen(1.0f - transform.localScale.y);
    }
}


