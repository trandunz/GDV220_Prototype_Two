using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuckGemFromUI : MonoBehaviour
{
    OxygenTankValue m_oxytank;
    AudioSource m_AudioSource;

    [SerializeField] AudioClip m_GemSlottedClip;
    [SerializeField] GameObject m_GemPrefab;
    [SerializeField] float m_LerpTime = 0.5f;

    void Start()
    {
        m_oxytank = FindObjectOfType<OxygenTankValue>();
        m_AudioSource = GetComponent<AudioSource>();

    }

    public void MakeGem(Vector3 _position)
    {
        StartCoroutine(CreateGemRoutine(_position));
    }

    IEnumerator CreateGemRoutine(Vector3 _position)
    {
        var newGem = Instantiate(m_GemPrefab, _position, Quaternion.identity);
        var startPos = newGem.transform.position;
        var startScale = newGem.transform.localScale;
        float lerpAmount = 0.0f;

        while (Vector3.Distance(newGem.transform.position, m_oxytank.GetNextOxygemSlot().position) > 0.2f)
        {
            newGem.transform.position = Vector3.Lerp(startPos, m_oxytank.GetNextOxygemSlot().position, lerpAmount);
            newGem.transform.localScale = Vector3.Lerp(startScale, Vector3.one * 0.25f, lerpAmount);
            lerpAmount += Time.deltaTime * m_LerpTime;
            yield return new WaitForEndOfFrame();
        }
        m_oxytank.AddOxygem();
        m_AudioSource.pitch = 1.5f + 0.1f * m_oxytank.iOxygemCount;
        m_AudioSource.PlayOneShot(m_GemSlottedClip);
        Destroy(newGem);
    }
}
