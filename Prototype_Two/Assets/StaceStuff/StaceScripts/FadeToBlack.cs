using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeToBlack : MonoBehaviour
{
    public Image UI_Image; // UI image to fade
    public Color objectColor;
    public TMPro.TextMeshProUGUI ReturnUpText;
    public Color textColor;
    public float fFadeAmount;
    public float fFadeSpeed = 0.5f;
    public float fPostFadeTimer = 2.0f;
    private float fTimer = 0;
    bool bFadeComplete = false;

    // Object to turn off/stop etc
    public GameObject screenCentre;

    public bool bFading = false; // Script wont run unless this is flipped

    // Start is called before the first frame update
    void Start()
    {
        objectColor = UI_Image.GetComponent<Image>().color;
    }

    // Update is called once per frame
    void Update()
    {
        if (bFading == true)
        {
            // Stop other objects
            screenCentre.GetComponent<CameraMovement>().fCameraSpeed = 0;

            if (UI_Image.GetComponent<Image>().color.a < 1)
            {
                fFadeAmount = objectColor.a + (fFadeSpeed * Time.deltaTime);

                objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fFadeAmount);
                UI_Image.GetComponent<Image>().color = objectColor;


                textColor = new Color(ReturnUpText.color.r, ReturnUpText.color.g, ReturnUpText.color.b, fFadeAmount);
                ReturnUpText.color = textColor;
            }

            //Add text
            //if (UI_Image.GetComponent<Image>().color.a >= 0.3)
            //{
            //    ReturnUpText.enabled = true;
            //}

            if (UI_Image.GetComponent<Image>().color.a >= 1)
            {
                bFadeComplete = true;
            }

            if (bFadeComplete == true)
            {
                fTimer = fTimer + 1 * Time.deltaTime;

                if (fTimer >= fPostFadeTimer)
                {
                    // Change scene
                    LevelLoader.instance.LoadLevel(0);
                }
            }
        }
    }
}
