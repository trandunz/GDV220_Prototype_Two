using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighscoreEntry : MonoBehaviour
{
    [SerializeField] TMPro.TextMeshProUGUI Score;
    [SerializeField] EnterHighScore Initials;
    [SerializeField] int score;

    public void SetScore(int _score)
    {
        Instantiate(Initials.gameObject);
        Initials.hse = this;
        Initials.isOn = true;
        score = _score;
    }

    public void Continue()
    {
        string scoreStr = PlayerPrefs.GetString("Initials") + score.ToString();
        Score.SetText(scoreStr);
    }
}
