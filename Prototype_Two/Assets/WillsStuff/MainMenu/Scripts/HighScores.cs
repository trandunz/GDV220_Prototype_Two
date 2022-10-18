using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighScores : MonoBehaviour
{
    [SerializeField] GameObject ContentArea;
    [SerializeField] GameObject HighscoreEntryPrefab;
    // Start is called before the first frame update
    void Start()
    {
        TopScores.LoadScores();
        GameObject highscore = Instantiate(HighscoreEntryPrefab, ContentArea.transform);
        int score = TopScores.CompareScore(PlayerPrefs.GetInt("DeepestDepth"));
        highscore.GetComponent<HighscoreEntry>().SetScore(score);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
