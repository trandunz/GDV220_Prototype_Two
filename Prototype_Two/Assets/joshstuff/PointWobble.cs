using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointWobble : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] float m_RockSpeed = 1.0f;
    [SerializeField] float m_VerticalBobSpeed = 1.0f;
    [SerializeField] float m_VerticalBobAmp = 1.0f;
    [SerializeField] float m_HorizontalBobSpeed = 5.0f;
    [SerializeField] float m_HorizontalBobAmp = 1.0f;

    Vector3 m_StartPos;
    // Start is called before the first frame update
    void Start()
    {
        m_StartPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = m_StartPos + new Vector3(Mathf.Cos(m_HorizontalBobSpeed * Time.time) * m_HorizontalBobAmp, Mathf.Sin(m_VerticalBobSpeed * Time.time) * m_VerticalBobAmp, 0.0f);
    }
}
