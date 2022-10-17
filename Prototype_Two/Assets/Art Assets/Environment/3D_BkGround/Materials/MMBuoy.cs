using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MMBuoy : MonoBehaviour
{
    [SerializeField] Material defualtLightMat;
    [SerializeField] Material glowingLightMat;
    [SerializeField] MeshRenderer lightmesh;
    [SerializeField] float m_FlashSpeed = 2.0f;
    float m_FlashTimer = 0.0f;
    bool IsSelected = false;
    bool lightOn = false;

    private void Start()
    {
        lightmesh.material = defualtLightMat;
    }
    private void Update()
    {
        if (IsSelected)
        {
            if (m_FlashTimer > 0)
                m_FlashTimer -= Time.deltaTime;

            else if (m_FlashTimer <= 0.0f)
            {
                m_FlashTimer = m_FlashSpeed;

                lightOn = !lightOn;

                if (lightOn)
                {
                    Debug.Log("Boy Selected");
                    lightmesh.material = glowingLightMat;
                }   
                else
                    lightmesh.material = defualtLightMat;
            }
                
        }
        else
        {
            lightOn = false;
            lightmesh.material = defualtLightMat;
        }
    }
    public void SetSelected(bool _selected)
    {
        IsSelected = _selected;

        if (!IsSelected)
            m_FlashTimer = 0.0f;
    }
}
