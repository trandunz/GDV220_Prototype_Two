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
        GameObject highscore = Instantiate(HighscoreEntryPrefab, ContentArea.transform);
        highscore.GetComponent<HighscoreEntry>().SetName("NAME:");
        highscore.GetComponent<HighscoreEntry>().SetScore(PlayerPrefs.GetInt("DeepestDepth"));
        Instantiate(HighscoreEntryPrefab, ContentArea.transform);
        Instantiate(HighscoreEntryPrefab, ContentArea.transform);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
