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
        GEMCHEST,
        FLARE
    };

    [SerializeField] BUFFTYPE m_BuffType = BUFFTYPE.RANDOM;

    public BUFFTYPE GetBuffType()
    {
        return m_BuffType;
    }

    public void GiveToPlayer(SwimController _player)
    {
        _player.PickupBubbleBuff(m_BuffType);
        Destroy(gameObject);
    }
}
