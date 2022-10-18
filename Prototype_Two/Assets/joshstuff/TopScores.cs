using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopScores : MonoBehaviour
{
    public struct ScoreValues
    {
        public int placement;
        public string initials;
        public string initialsPlayerPrefName;
        public int score;
        public string scorePlayerPrefName;
    }

    public static ScoreValues[] scores;

    static public void LoadScores()
    {
        scores = new ScoreValues[10];

        for (int i = 0; i < 10; i++)
        {
            scores[i].placement = i + 1;
        }

        scores[0].initialsPlayerPrefName = "FirstPlaceInitials";
        scores[0].scorePlayerPrefName = "FirstPlaceDeepestDepth";
        scores[0].initials = PlayerPrefs.GetString("FirstPlaceInitials");
        scores[0].score = PlayerPrefs.GetInt("FirstPlaceDeepestDepth");

        scores[1].initialsPlayerPrefName = "SecondPlaceInitials";
        scores[1].scorePlayerPrefName = "SecondPlaceDeepestDepth";
        scores[1].initials = PlayerPrefs.GetString("SecondPlaceInitials");
        scores[1].score = PlayerPrefs.GetInt("SecondPlaceDeepestDepth");

        scores[2].initialsPlayerPrefName = "ThirdPlaceInitials";
        scores[2].scorePlayerPrefName = "ThirdPlaceDeepestDepth";
        scores[2].initials = PlayerPrefs.GetString("ThirdPlaceInitials");
        scores[2].score = PlayerPrefs.GetInt("ThirdPlaceDeepestDepth");

        scores[3].initialsPlayerPrefName = "FourthPlaceInitials";
        scores[3].scorePlayerPrefName = "FourthPlaceDeepestDepth";
        scores[3].initials = PlayerPrefs.GetString("FourthPlaceInitials");
        scores[3].score = PlayerPrefs.GetInt("FourthPlaceDeepestDepth");

        scores[4].initialsPlayerPrefName = "FifthPlaceInitials";
        scores[4].scorePlayerPrefName = "FifthPlaceDeepestDepth";
        scores[4].initials = PlayerPrefs.GetString("FifthPlaceInitials");
        scores[4].score = PlayerPrefs.GetInt("FifthPlaceDeepestDepth");

        scores[5].initialsPlayerPrefName = "SixthPlaceInitials";
        scores[5].scorePlayerPrefName = "SixthPlaceDeepestDepth";
        scores[5].initials = PlayerPrefs.GetString("SixthPlaceInitials");
        scores[5].score = PlayerPrefs.GetInt("SixthPlaceDeepestDepth");

        scores[6].initialsPlayerPrefName = "SeventhPlaceInitials";
        scores[6].scorePlayerPrefName = "SeventhPlaceDeepestDepth";
        scores[6].initials = PlayerPrefs.GetString("SeventhPlaceInitials");
        scores[6].score = PlayerPrefs.GetInt("SeventhPlaceDeepestDepth");

        scores[7].initialsPlayerPrefName = "EigthPlaceInitials";
        scores[7].scorePlayerPrefName = "EigthPlaceDeepestDepth";
        scores[7].initials = PlayerPrefs.GetString("EigthPlaceInitials");
        scores[7].score = PlayerPrefs.GetInt("EigthPlaceDeepestDepth");

        scores[8].initialsPlayerPrefName = "NinthPlaceInitials";
        scores[8].scorePlayerPrefName = "NinthPlaceDeepestDepth";
        scores[8].initials = PlayerPrefs.GetString("NinthPlaceInitials");
        scores[8].score = PlayerPrefs.GetInt("NinthPlaceDeepestDepth");

        scores[9].initialsPlayerPrefName = "TenthPlaceInitials";
        scores[9].scorePlayerPrefName = "TenthPlaceDeepestDepth";
        scores[9].initials = PlayerPrefs.GetString("TenthPlaceInitials");
        scores[9].score = PlayerPrefs.GetInt("TenthPlaceDeepestDepth");
    }

    static void ChangeScore(int num1, int num2)
    {
        scores[num1].score = scores[num2].score;
        scores[num1].initials = scores[num2].initials;
        PlayerPrefs.SetInt(scores[num1].scorePlayerPrefName, scores[num1].score);
        PlayerPrefs.SetString(scores[num1].initialsPlayerPrefName, scores[num1].initials);
    }

    public static int CompareScore(int _score)
    {
        for (int i = 0; i < scores.Length; i++)
        {
            if (scores[i].score < _score)
            {
                if (i < 9)
                {
                    for (int j = 9; j > i; j--)
                    {
                        ChangeScore(j, j - 1);
                    }
                }
                Debug.Log(i);
                return i;
            }
        }
        return 10;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
