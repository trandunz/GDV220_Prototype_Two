using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HighScoreMenu : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI[] texts;
    // Start is called before the first frame update
    void Start()
    {
        TopScores.LoadScores();
        for (int i = 0; i < texts.Length; i++)
        {
            texts[i].text = PlayerPrefs.GetString(TopScores.scores[i].initialsPlayerPrefName) + PlayerPrefs.GetInt(TopScores.scores[i].scorePlayerPrefName) + "m";
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Return) || Input.GetKeyUp(KeyCode.Backspace))
        {
            gameObject.SetActive(false);
        }
    }
}
