using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ink : MonoBehaviour
{
    ParticleSystem m_ParticleSystem;
    SwimController[] m_Players;
    private void Start()
    {
        m_ParticleSystem = GetComponent<ParticleSystem>();
        m_Players = FindObjectsOfType<SwimController>();
        m_ParticleSystem.trigger.SetCollider(0, m_Players[0].GetComponent<BoxCollider>());
        m_ParticleSystem.trigger.SetCollider(1, m_Players[1].GetComponent<BoxCollider>());
    }
    void OnParticleTrigger()
    {
        List<ParticleSystem.Particle> stay = new List<ParticleSystem.Particle>();
        m_ParticleSystem.GetTriggerParticles(ParticleSystemTriggerEventType.Inside, stay);

        List<ParticleSystem.Particle> enter = new List<ParticleSystem.Particle>();
        m_ParticleSystem.GetTriggerParticles(ParticleSystemTriggerEventType.Enter, enter);

        foreach (var particle in stay)
        {
            foreach (var player in m_Players)
            {
                BoxCollider collider = player.GetComponent<BoxCollider>();
                if (collider.bounds.Contains(particle.position))
                {
                    player.Slow();
                }
            }
        }
        foreach (var particle in enter)
        {
            foreach (var player in m_Players)
            {
                BoxCollider collider = player.GetComponent<BoxCollider>();
                if (collider.bounds.Contains(particle.position))
                {
                    player.Slow();
                }
            }
        }
    }
}
