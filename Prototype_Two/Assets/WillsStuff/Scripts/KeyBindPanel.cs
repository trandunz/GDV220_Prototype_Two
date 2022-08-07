using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyBindPanel : MonoBehaviour
{
    [SerializeField] string BindName;
    [SerializeField] TMPro.TextMeshProUGUI BindText;
    [SerializeField] KeyCode CurrentBind = KeyCode.None;
    bool ResettingBind = false;

    private void Start()
    {
        if ((KeyCode)PlayerPrefs.GetInt(BindName) == KeyCode.None)
        {
            PlayerPrefs.SetInt(BindName, (int)CurrentBind);
        }
        else
        {
            CurrentBind = (KeyCode)PlayerPrefs.GetInt(BindName);
        }
        BindText.text = CurrentBind.ToString();
    }

    public void ClearBind()
    {
        if (!ResettingBind)
        {
            BindText.text = "Press Key To Set";
            ResettingBind = true;
            StartCoroutine(GrabBindInput());
        }
        else
        {
            BindText.text = CurrentBind.ToString();
            ResettingBind = false;
            StopCoroutine(GrabBindInput());
        }
    }

    IEnumerator GrabBindInput()
    {
        while (ResettingBind)
        {
            foreach (KeyCode kcode in System.Enum.GetValues(typeof(KeyCode)))
            {
                if (Input.GetKeyDown(kcode))
                {
                    CurrentBind = kcode;
                    ResettingBind = false;
                }
            }
            yield return null;
        }
        yield return new WaitUntil(() => ResettingBind == false);
        BindText.text = CurrentBind.ToString();
        PlayerPrefs.SetInt(BindName, (int)CurrentBind);
    }
}
