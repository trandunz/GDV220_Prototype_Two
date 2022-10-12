using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Jellyfish : MonoBehaviour
{
    GameObject OxygenTank;
    Animator animator;
    public float fTimer = 0.0f;
    public float fMinRange = 0.0f;
    public float fMaxRange = 0.2f;

    bool bMoving = true;
    public float fMoveSpeed;

    // Start is called before the first frame update
    void Start()
    {
        fTimer = Random.Range(fMinRange, fMaxRange);
        animator = GetComponentInChildren<Animator>();
        OxygenTank = GameObject.FindGameObjectWithTag("SceneCentre");
    }

    // Update is called once per frame
    void Update()
    {
        
        fTimer -= Time.deltaTime; // Increase timer by 1 second
        
        if (fTimer < 0.0f)
        {
            animator.SetTrigger("Swim");
            fTimer = Random.Range(fMinRange, fMaxRange);
        }

        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Jellyfish_Swim"))
        {
            float animationTime = animator.GetCurrentAnimatorStateInfo(0).normalizedTime;
            if (animationTime > 0.5 && animationTime < 1.0f)
            {
                bMoving = false;
                Debug.Log(animationTime);
            }
        }
        else
        {
            bMoving = true;
        }

        if (bMoving)
        {
            //transform.Translate(0.0f, 0.0f, 0.0f); // Stop movement (looks like moving)
            transform.Translate(0.0f, OxygenTank.GetComponentInParent<CameraMovement>().fCameraSpeed * fMoveSpeed * Time.deltaTime, 0.0f);// slows movement (looks like moving)
            
            // "lerps" without lerping
            fMoveSpeed -= Time.deltaTime;
            if (fMoveSpeed < 0.0f)
            {
                fMoveSpeed = 0.0f;
            }
        }
        else
        {
            // Movement follows camera speed - makes jellyfish look like they stop moving
            transform.Translate(0.0f, OxygenTank.GetComponentInParent<CameraMovement>().fCameraSpeed * Time.deltaTime, 0.0f);
            fMoveSpeed = 1.0f;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag is "Player")
        {
            other.GetComponent<SwimController>().Paralyze();
        }
    }
}
