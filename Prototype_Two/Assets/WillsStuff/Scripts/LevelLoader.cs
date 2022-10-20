using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelLoader : MonoBehaviour
{
    public static LevelLoader instance;
    [SerializeField] Image FrontWater;
    [SerializeField] Image BackWater;
    [SerializeField] Image Boat;
    Vector2 frontwaterStartPos;
    Vector2 backWaterStartPos;
    Vector2 boatStartPos;
    Quaternion boatStartRotaion;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

    }

    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Keypad0))
        //{
        //    PlayerPrefs.DeleteAll();
        //}
    }

    public void LoadLevel(int _scene)
    {
        StartCoroutine(LoadAsync(_scene));
    }

    IEnumerator LoadAsync(int _scene)
    {
        DontDestroyOnLoad(instance);
        SceneManager.LoadScene(2);

        yield return new WaitForSeconds(0.2f);
        Slider progressSlider = FindObjectOfType<Slider>();
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(_scene);

        if (FrontWater != null)
            frontwaterStartPos = FrontWater.rectTransform.anchoredPosition;

        if (BackWater != null)
            backWaterStartPos = BackWater.rectTransform.anchoredPosition;

        if (Boat != null)
        {
            boatStartRotaion = Boat.rectTransform.localRotation;
            boatStartPos = Boat.rectTransform.anchoredPosition;
        }
        while (!asyncOperation.isDone)
        {
            if (FrontWater != null)
                FrontWater.rectTransform.anchoredPosition = frontwaterStartPos + Vector2.right * Mathf.Sin(Time.time * 5.0f) * 6.0f;

            if (BackWater != null)
                BackWater.rectTransform.anchoredPosition = backWaterStartPos - Vector2.right * Mathf.Sin(Time.time * 2.5f) * 3.0f;

            if (Boat != null)
            {
                Boat.rectTransform.anchoredPosition = boatStartPos + Vector2.up * Mathf.Sin(Time.time * 6.0f) * 1.5f;
                Boat.rectTransform.localRotation = boatStartRotaion * Quaternion.Euler(Vector3.forward * Mathf.Sin(Time.time * 6.0f) * 2.0f);
            }

            if (progressSlider == null)
                progressSlider = FindObjectOfType<Slider>();
            float progress = Mathf.Clamp01(asyncOperation.progress / 0.9f);
            if (progressSlider != null)
            {
                if (progress < 0.955)
                    progressSlider.value = progress;
            }
            yield return null;
        }
    }
}
