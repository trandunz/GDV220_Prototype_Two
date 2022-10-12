using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eel : MonoBehaviour
{
    [SerializeField] float fMoveSpeed = 2.0f;
    [SerializeField] float PeekDistance = 0.3f;
    [SerializeField] float PeekTime = 0.5f;
    [SerializeField] float PeakDelay = 1.0f;
    [SerializeField] GameObject BackgroundEelPrefab;
    [SerializeField] float KnockBackForce;

    GameObject BackgroundEel;
    Vector3 StartPos;
    bool bMoving = false;
    bool bPeeking = false;
    bool m_MoveRight = true;

    private void Start()
    {
        BackgroundEel = Instantiate(BackgroundEelPrefab, transform);
        BackgroundEel.transform.rotation = Quaternion.Euler(BackgroundEel.transform.rotation.eulerAngles.x, BackgroundEel.transform.rotation.eulerAngles.y + 180, BackgroundEel.transform.rotation.eulerAngles.z);
        BackgroundEel.transform.position = new Vector3(BackgroundEel.transform.position.x, BackgroundEel.transform.position.y, 7.0f); 
        BackgroundEel.transform.position = BackgroundEel.transform.position - transform.right * 10.0f;
        BackgroundEel.transform.localScale = new Vector3(0.7f, 0.7f, 0.3f);
        BackgroundEel.transform.position = BackgroundEel.transform.position + Vector3.up * 2.0f;
        BackgroundEel.transform.SetParent(null);
        Destroy(BackgroundEel, 10.0f);
    }

    public void SetDirection(bool _moveRight)
    {
        m_MoveRight = _moveRight;
    }

    public void StartMoving()
    {
        bMoving = true;
    }

    public void Peek()
    {
        StartPos = transform.position;
        if (!bPeeking)
        {
            StartCoroutine(PeekRoutine());
        }
    }

    IEnumerator PeekRoutine()
    {
        bPeeking = true;

        yield return new WaitForSeconds(PeakDelay);

        float peekLerp = 0.0f ;
        while (peekLerp < 1.0f)
        {
            if (m_MoveRight)
                transform.position = Vector3.Lerp(new Vector3(StartPos.x, StartPos.y, StartPos.z), new Vector3(StartPos.x + (PeekDistance), StartPos.y, StartPos.z), peekLerp);
            else
                transform.position = Vector3.Lerp(new Vector3(StartPos.x, StartPos.y, StartPos.z), new Vector3(StartPos.x - (PeekDistance), StartPos.y, StartPos.z), peekLerp);
            peekLerp += Time.deltaTime * (PeekTime / 2);
            yield return new WaitForEndOfFrame();
        }
        while (peekLerp > 0.0f)
        {
            if (m_MoveRight)
                transform.position = Vector3.Lerp(new Vector3(StartPos.x, StartPos.y, StartPos.z), new Vector3(StartPos.x + (PeekDistance), StartPos.y, StartPos.z), peekLerp);
            else
                transform.position = Vector3.Lerp(new Vector3(StartPos.x, StartPos.y, StartPos.z), new Vector3(StartPos.x - (PeekDistance), StartPos.y, StartPos.z), peekLerp);
            peekLerp -= Time.deltaTime * (PeekTime / 2);
            yield return new WaitForEndOfFrame();
        }

        bPeeking = false;
        StartMoving();
    }

    // Update is called once per frame
    void Update()
    {
        if (BackgroundEel)
            BackgroundEel.transform.position += transform.right * fMoveSpeed * 0.5f * Time.deltaTime;

        if (bMoving)
            transform.Translate(new Vector3(-fMoveSpeed * Time.deltaTime, 0, 0));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag is "Player")
        {
            Vector3 player_pos = other.GetComponent<Transform>().position;

            Vector3 impulse = new Vector3(-fMoveSpeed, 0.0f, 0.0f);
            if (m_MoveRight)
            { 
                impulse = new Vector3(fMoveSpeed, 0.0f, 0.0f);
            }
            impulse = impulse.normalized;
            if (player_pos.y > transform.position.y)
            {
                impulse += new Vector3(0.0f, 1.0f, 0.0f);
            }
            else
            {
                impulse += new Vector3(0.0f, -1.0f, 0.0f);
            }
            impulse *= KnockBackForce;
            other.GetComponent<SwimController>().ApplyImpulse(impulse);
            
            other.GetComponent<SwimController>().HitEnemy();
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.tag is "Player")
        {
            other.GetComponent<SwimController>().StartInvulnrability();
        }
    }
}
