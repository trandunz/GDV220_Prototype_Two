using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighscoreEntry : MonoBehaviour
{
    [SerializeField] TMPro.TextMeshProUGUI Score;

    public void SetScore(int _score)
    {
        Score.SetText(_score.ToString());
    }
}
