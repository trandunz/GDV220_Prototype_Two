using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EnterHighScore : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI[] characters;
    [SerializeField] int[] charactersCurrentPos;
    [SerializeField] float delayBetweenInput = 0.12f;
    [SerializeField] float counterDelayBetweenInput = 0.0f;
    [SerializeField] GameObject underLine;
    int currentTextMesh = 0;
    char[] chars;
    public bool isOn = false;
    public HighscoreEntry hse;
    public MainMenuButtons mmb;

    // Start is called before the first frame update
    void Start()
    {
        charactersCurrentPos = new int[characters.Length];
        for (int i = 0; i < charactersCurrentPos.Length; i++)
            charactersCurrentPos[i] = 0;
        string str = "ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890?!@#$%^&*()-_+=[]{}:;,.<>/";
        chars = str.ToCharArray();
        hse = FindObjectOfType<HighscoreEntry>();
        mmb = FindObjectOfType<MainMenuButtons>();
        mmb.pauseStart = true;
        isOn = true;

        underLine.transform.position = new Vector2(characters[currentTextMesh].transform.position.x, characters[currentTextMesh].transform.position.y - 18);
    }

    // Update is called once per frame
    void Update()
    {
        if (counterDelayBetweenInput > 0)
        {
            counterDelayBetweenInput -= Time.deltaTime;
        }
        if (counterDelayBetweenInput <= 0 && isOn)
        {
            // down
            if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
            {
                charactersCurrentPos[currentTextMesh]++;
                if (charactersCurrentPos[currentTextMesh] >= chars.Length)
                {
                    charactersCurrentPos[currentTextMesh] = 0;
                }

                characters[currentTextMesh].SetText(chars[charactersCurrentPos[currentTextMesh]].ToString());
                counterDelayBetweenInput = delayBetweenInput;
            }

            // up
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            {
                charactersCurrentPos[currentTextMesh]--;
                if (charactersCurrentPos[currentTextMesh] < 0)
                {
                    charactersCurrentPos[currentTextMesh] = chars.Length - 1;
                }

                characters[currentTextMesh].SetText(chars[charactersCurrentPos[currentTextMesh]].ToString());
                counterDelayBetweenInput = delayBetweenInput;
            }

            // left
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            {
                currentTextMesh--;
                if (currentTextMesh < 0)
                {
                    currentTextMesh = characters.Length - 1;
                }

                underLine.transform.position = new Vector2(characters[currentTextMesh].transform.position.x, characters[currentTextMesh].transform.position.y - 18);

                counterDelayBetweenInput = delayBetweenInput;
            }

            // right
            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            {
                currentTextMesh++;
                if (currentTextMesh >= characters.Length)
                {
                    currentTextMesh = 0;
                }

                underLine.transform.position = new Vector2(characters[currentTextMesh].transform.position.x, characters[currentTextMesh].transform.position.y - 18);

                counterDelayBetweenInput = delayBetweenInput;
            }

            // enter
            if (Input.GetKeyDown(KeyCode.Return))
            {
                string str = "";
                for (int i = 0; i < characters.Length; i++)
                {
                    str += characters[i].GetParsedText();
                }
                str += ": ";

                PlayerPrefs.SetString("Initials", str);
                hse.Continue();
                mmb.pauseStart = false;
                Destroy(gameObject);

                counterDelayBetweenInput = delayBetweenInput;
            }
        }
    }
}
