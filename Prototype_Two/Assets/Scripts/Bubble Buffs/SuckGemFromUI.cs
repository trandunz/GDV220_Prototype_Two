using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuckGemFromUI : MonoBehaviour
{
    OxygenTankValue m_oxytank;

    [SerializeField] GameObject m_GemPrefab;
    [SerializeField] float m_LerpTime = 0.5f;

    void Start()
    {
        m_oxytank = FindObjectOfType<OxygenTankValue>();

    }

    public void MakeGem(Vector3 _position)
    {
        StartCoroutine(CreateGemRoutine(_position));
    }

    IEnumerator CreateGemRoutine(Vector3 _position)
    {
        var newGem = Instantiate(m_GemPrefab, _position, Quaternion.identity);
        var startPos = newGem.transform.position;

        float lerpAmount = 0.0f;

        while (Vector3.Distance(newGem.transform.position, m_oxytank.transform.position) > 0)
        {
            newGem.transform.position = Vector3.Lerp(startPos, m_oxytank.transform.position, lerpAmount);
            lerpAmount += Time.deltaTime * m_LerpTime;
            yield return new WaitForEndOfFrame();
        }
        Destroy(newGem);
    }
}
