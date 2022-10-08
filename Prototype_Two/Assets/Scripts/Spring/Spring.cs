using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spring : MonoBehaviour
{
    public float m_strength;
    public float m_resting_distance;
    public Particle m_particle1;
    public Particle m_particle2;

    public void UpdateSpring()
    {
        Vector3 force = m_particle2.transform.position - m_particle1.transform.position;
        float x = force.magnitude - m_resting_distance;
        force = force.normalized;
        force *= m_strength * x;

        m_particle1.ApplyForce(force);
        force *= -1.0f;
        m_particle2.ApplyForce(force);
    }
}
