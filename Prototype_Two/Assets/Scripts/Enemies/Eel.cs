using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eel : MonoBehaviour
{
    [SerializeField] float fMoveSpeed = 2.0f;
    [SerializeField] float PeekDistance = 0.3f;
    [SerializeField] float PeekTime = 0.5f;
    [SerializeField] float PeakDelay = 1.0f;

    Vector3 StartPos;
    bool bMoving = false;
    bool bPeeking = false;
    bool m_MoveRight = true;

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
        while (peekLerp > 0)
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
        if (bMoving && m_MoveRight)
            transform.Translate(new Vector3(-fMoveSpeed * Time.deltaTime, 0, 0));
        else if (bMoving)
            transform.Translate(new Vector3(-fMoveSpeed * Time.deltaTime, 0, 0));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag is "Player")
        {
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
