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
            EnterHighScore high = Instantiate(Initials.gameObject).GetComponent<EnterHighScore>();
            high.Initialise(score);
        }
        else
        {
            Continue();
        }
        newHighscore = false;
    }

    public void Continue()
    {
        string scoreStr = PlayerPrefs.GetString(TopScores.scores[0].initialsPlayerPrefName) + PlayerPrefs.GetInt(TopScores.scores[0].scorePlayerPrefName).ToString() + "m";
        Score.SetText(scoreStr);
    }
}
