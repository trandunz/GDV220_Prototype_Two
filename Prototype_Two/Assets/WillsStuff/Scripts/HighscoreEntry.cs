using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighscoreEntry : MonoBehaviour
{
    [SerializeField] TMPro.TextMeshProUGUI Name;
    [SerializeField] TMPro.TextMeshProUGUI Score;

    public void SetName(string _name)
    {
        Score.SetText(_name);
    }

    public void SetScore(int _score)
    {
        Score.SetText(_score.ToString());
    }
}
