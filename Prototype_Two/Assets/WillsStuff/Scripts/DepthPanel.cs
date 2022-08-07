using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DepthPanel : MonoBehaviour
{
    [SerializeField] TMPro.TextMeshProUGUI DepthText;

    private void Update()
    {
        int depth = Mathf.Abs((int)Camera.main.transform.position.y);
        DepthText.text = depth.ToString() + "m";

        if (depth > PlayerPrefs.GetInt("DeepestDepth"))
        {
            PlayerPrefs.SetInt("DeepestDepth", depth);
        }
    }
}
