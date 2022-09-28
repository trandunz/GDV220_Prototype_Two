using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleBuff : MonoBehaviour
{
    public enum BUFFTYPE
    {
        UNASSIGNED = 0,

        RANDOM,
        SHIELD,
        MAGNET,
        FLARE,
        GEMCHEST
    };

    [SerializeField] BUFFTYPE m_BuffType = BUFFTYPE.RANDOM;
    SwimController m_Player;

    private void Start()
    {
        m_Player = FindObjectOfType<SwimController>();
    }
    public BUFFTYPE GetBuffType()
    {
        return m_BuffType;
    }

    public void GiveToPlayer()
    {
        m_Player.PickupBubbleBuff(m_BuffType);
        Destroy(gameObject);
    }
}
