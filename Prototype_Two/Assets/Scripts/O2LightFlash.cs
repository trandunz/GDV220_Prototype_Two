using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class O2LightFlash : MonoBehaviour
{
    [SerializeField] Light o2Light;
    [SerializeField] float PulseIntensity = 0.5f;
    [SerializeField] float PulseSpeed = 2.0f;

    public void Pulse()
    {
        StopAllCoroutines();
        StartCoroutine(PulseRoutine());
    }

    IEnumerator PulseRoutine()
    {
        float intensity = 0.3f;

        float ratio = 0.0f;
        while (o2Light.intensity < PulseIntensity)
        {
            o2Light.intensity = Mathf.Lerp(intensity, PulseIntensity, ratio);
            ratio += Time.deltaTime * PulseSpeed;
            yield return new WaitForEndOfFrame();
        }
        ratio = 1.0f;
        while (o2Light.intensity > intensity)
        {
            o2Light.intensity = Mathf.Lerp(intensity, PulseIntensity, ratio);
            ratio -= Time.deltaTime * PulseSpeed;
            yield return new WaitForEndOfFrame();
        }

    }
}
