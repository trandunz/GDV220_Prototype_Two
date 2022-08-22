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
        highscore.GetComponent<HighscoreEntry>().SetScore(PlayerPrefs.GetInt("DeepestDepth"));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
