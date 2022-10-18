using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighscoreEntry : MonoBehaviour
{
    [SerializeField] TMPro.TextMeshProUGUI Score;
    [SerializeField] EnterHighScore Initials;
    [SerializeField] int score;
    static public bool newHighscore = false;

    public void SetScore(int _score)
    {
        score = _score;
        if (newHighscore)
        {
            Instantiate(Initials.gameObject);
        }
        else
        {
            Continue();
        }
        newHighscore = false;
    }

    public void Continue()
    {
        string scoreStr = PlayerPrefs.GetString("Initials") + score.ToString() + "m";
        Score.SetText(scoreStr);
    }
}
