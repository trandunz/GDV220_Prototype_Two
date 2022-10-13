using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkingLight : MonoBehaviour
{
    [SerializeField] float BlinkSpeed = 3.0f;
    [SerializeField] float BlinkIntensity = 3.0f;
    Light m_Light;
    float m_Intensity;
    private void Start()
    {
        m_Light = GetComponent<Light>();
        m_Intensity = m_Light.intensity;
    }
    void Update()
    {
        m_Light.intensity = m_Intensity + Mathf.Sin(Time.time * BlinkSpeed) * BlinkIntensity;
    }
}
