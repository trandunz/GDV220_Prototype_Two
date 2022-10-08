using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle : MonoBehaviour
{
    public Vector3 m_acceleration;
    public float m_mass;
    public float m_limit;
    public float m_speed;
    public Vector3 m_velocity;

    private void Start()
    {
        m_velocity = new Vector3();
        m_acceleration = new Vector3();
    }

    public void ApplyForce(Vector3 force)
    {
        m_acceleration += force / m_mass;
    }

    private void Update()
    {
        m_velocity = m_velocity + m_acceleration;
        m_velocity = Limit(m_velocity);
        Vector3 pos = gameObject.transform.position;
        pos += m_velocity * m_speed * Time.deltaTime;
        pos.z = -1.0f;
        gameObject.transform.position = pos;

        m_acceleration = new Vector3();

    }

    private Vector3 Limit(Vector3 source)
    {
        float length = Mathf.Sqrt((source.x * source.x) + (source.y * source.y) + (source.z * source.z));
        if (length > m_limit)
        {
            return source.normalized * m_limit;
        }
        else
        {
            return source;
        }
    }
}
