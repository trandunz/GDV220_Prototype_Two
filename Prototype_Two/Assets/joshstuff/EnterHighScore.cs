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
    int currentTextMesh = 0;
    char[] chars;

    // Start is called before the first frame update
    void Start()
    {
        charactersCurrentPos = new int[characters.Length];
        for (int i = 0; i < charactersCurrentPos.Length; i++)
            charactersCurrentPos[i] = 0;
        string str = "ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890?!@#$%^&*()-_+=[]{}:;,.<>/";
        chars = str.ToCharArray();
    }

    // Update is called once per frame
    void Update()
    {
        if (counterDelayBetweenInput > 0)
        {
            counterDelayBetweenInput -= Time.deltaTime;
        }
        if (counterDelayBetweenInput <= 0)
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

                counterDelayBetweenInput = delayBetweenInput;
            }
        }
    }
}
