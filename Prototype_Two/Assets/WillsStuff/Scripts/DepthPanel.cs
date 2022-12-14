using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DepthPanel : MonoBehaviour
{
    [SerializeField] TMPro.TextMeshProUGUI ScoreText;
    int score = 0;

    private void Update()
    {
        int depth = Mathf.Abs((int)Camera.main.transform.position.y);
        depth += score;
        ScoreText.text = depth.ToString();

        if (depth > PlayerPrefs.GetInt(TopScores.scores[TopScores.scores.Length - 1].scorePlayerPrefName))
        {
            PlayerPrefs.SetInt("DeepestDepth", depth);
            HighscoreEntry.newHighscore = true;
        }
    }

    public int GetScore()
    {
        return Mathf.Abs((int)Camera.main.transform.position.y);
    }
    public void AddScore(int _amount)
    {
        score += _amount ;
    }
}
