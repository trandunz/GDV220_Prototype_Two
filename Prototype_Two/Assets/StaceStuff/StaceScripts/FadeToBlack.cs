using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeToBlack : MonoBehaviour
{
    public Image UI_Image; // UI image to fade
    //[SerializeField] TMPro.TextMeshProUGUI ReturnUpText;
    //public GameObject FadeToBlackObject;
    public Color objectColor;
    public float fFadeAmount;
    public float fFadeSpeed = 0.5f;

    public bool bFading = false; // Script wont run unless this is flipped

    //public string sceneName; // Scene to transition to

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
            //FadeToBlackObject.SetActive(true);
            if (UI_Image.GetComponent<Image>().color.a < 1)
            {
                fFadeAmount = objectColor.a + (fFadeSpeed * Time.deltaTime);

                objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fFadeAmount);
                UI_Image.GetComponent<Image>().color = objectColor;
            }

            //Add text
            //if (UI_Image.GetComponent<Image>().color.a >= 0.4)
           // {
            //    ReturnUpText.SetActive(true);
            //}

            if (UI_Image.GetComponent<Image>().color.a >= 1)
            {
                // Change scene

            }
        }

        // Testing - forces fade to black
        if (Input.GetKeyDown(KeyCode.B))
        {
            bFading = true;
        }

    }
}
