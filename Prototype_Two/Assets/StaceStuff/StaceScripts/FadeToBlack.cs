using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeToBlack : MonoBehaviour
{
    public Image UI_Image; // UI image to fade
    public Color objectColor;
    [SerializeField] TMPro.TextMeshProUGUI AnyKeyToContinue;
    [SerializeField] TMPro.TextMeshProUGUI DepthText;
    [SerializeField] TMPro.TextMeshProUGUI Highscore;
    public Color textColor;
    public float fFadeAmount;
    public float fFadeSpeed = 0.5f;
    public float fPostFadeTimer = 2.0f;
    private float fTimer = 0;
    bool bFadeComplete = false;
    int highscore;
    PauseMenuPanel pauseMenu;

    // Object to turn off/stop etc
    public GameObject screenCentre;

    public bool bFading = false; // Script wont run unless this is flipped

    // Start is called before the first frame update
    void Start()
    {
        objectColor = UI_Image.GetComponent<Image>().color;
        highscore = PlayerPrefs.GetInt("DeepestDepth");
        pauseMenu = FindObjectOfType<PauseMenuPanel>();
    }

    // Update is called once per frame
    void Update()
    {
        if (bFading == true)
        {
            pauseMenu.OnResume();

            // Stop other objects
            screenCentre.GetComponent<CameraMovement>().fCameraSpeed = 0;

            if (UI_Image.GetComponent<Image>().color.a < 1)
            {
                fFadeAmount = objectColor.a + (fFadeSpeed * Time.deltaTime);

                objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fFadeAmount);
                UI_Image.GetComponent<Image>().color = objectColor;


                textColor = new Color(DepthText.color.r, DepthText.color.g, DepthText.color.b, fFadeAmount);

                AnyKeyToContinue.color = textColor;
                DepthText.color = textColor;
                DepthText.text = "Depth\n" + FindObjectOfType<DepthPanel>().GetScore().ToString() + "m";

                if (FindObjectOfType<DepthPanel>().GetScore() >= highscore)
                    Highscore.color = new Color(Highscore.color.r, Highscore.color.g, Highscore.color.b, fFadeAmount);
            }

            //Add text
            //if (UI_Image.GetComponent<Image>().color.a >= 0.3)
            //{
            //    ReturnUpText.enabled = true;
            //}

            if (UI_Image.GetComponent<Image>().color.a >= 1)
            {
                if (bFadeComplete == false)
                {
                    int depth = FindObjectOfType<DepthPanel>().GetScore();
                    DepthText.text = "Depth\n" + depth.ToString() + "m";
                        
                    bFadeComplete = true;
                }
            }

            if (bFadeComplete == true)
            {
                if (Input.anyKeyDown)
                {
                    pauseMenu.OnResume();
                    LevelLoader.instance.LoadLevel(0);
                }
            }
        }
    }
}
