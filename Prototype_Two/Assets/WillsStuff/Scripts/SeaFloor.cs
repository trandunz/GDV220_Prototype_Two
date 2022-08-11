using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeaFloor : MonoBehaviour
{
    [SerializeField] Animator ChestAnimator;

    public void OpenChest()
    {
        ChestAnimator.SetBool("Open", true);
    }
}
