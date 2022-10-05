using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShake : MonoBehaviour
{
    Camera camera;
    float m_ShakeTimer;
    [SerializeField] float m_ShakeSpeed = 1.0f;
    [SerializeField] float m_ShakeTime = 0.5f;
    float m_ShakeAmplitude = 1.0f;
    Quaternion CameraStartRot;

    private void Start()
    {
        camera = Camera.main;
        CameraStartRot = camera.transform.rotation;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
            StartShake(10.0f);

        if (m_ShakeTimer > 0)
        {
            m_ShakeTimer -= Time.deltaTime;
            ShakeCamera();
        }
        else
        {
            LerpCameraBackToStart();
        }
    }
    public void StartShake(float _amplitude)
    {
        m_ShakeAmplitude = _amplitude;
        m_ShakeTimer = m_ShakeTime;
    }

    public void StartShake(float _amplitude, float _shakeTime)
    {
        m_ShakeAmplitude = _amplitude;
        m_ShakeTimer = _shakeTime;
    }

    void ShakeCamera()
    {
        transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, Mathf.Sin(Time.time * m_ShakeSpeed) * m_ShakeAmplitude);
    }
    void LerpCameraBackToStart()
    {
        transform.rotation = Quaternion.RotateTowards(transform.rotation, CameraStartRot, Time.deltaTime * m_ShakeSpeed);
    }
}
