using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeaFloor : MonoBehaviour
{
    [SerializeField] Animator ChestAnimator;
    [SerializeField] GameObject EndGameText;
    bool isOpen = false;

    public void OpenChest()
    {
        if (!isOpen)
        {
            isOpen = true;
            ChestAnimator.SetBool("Open", true);
            StartCoroutine(ChestCoroutine());
        }
    }

    IEnumerator ChestCoroutine()
    {
        EndGameText.SetActive(true);
        yield return new WaitForSeconds(3.0f);
        LevelLoader.instance.LoadLevel(1);
    }
}
