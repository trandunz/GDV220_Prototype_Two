using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelLoader : MonoBehaviour
{
    public static LevelLoader instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Keypad0))
        {
            PlayerPrefs.DeleteAll();
        }
    }

    public void LoadLevel(int _scene)
    {
        StartCoroutine(LoadAsync(_scene));
    }

    IEnumerator LoadAsync(int _scene)
    {
        DontDestroyOnLoad(instance);
        SceneManager.LoadScene(3);

        yield return new WaitForSeconds(0.2f);
        Slider progressSlider = FindObjectOfType<Slider>();
        TMPro.TextMeshProUGUI progressText = FindObjectOfType<TMPro.TextMeshProUGUI>();

        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(_scene);

        while(!asyncOperation.isDone)
        {
            if (progressSlider == null)
                progressSlider = FindObjectOfType<Slider>();
            if (progressText == null)
                progressText = FindObjectOfType<TMPro.TextMeshProUGUI>();
            float progress = Mathf.Clamp01(asyncOperation.progress / 0.9f);
            if (progressSlider != null)
                progressSlider.value = progress;
            if (progressText != null)
                progressText.text = (progress * 100).ToString() + "%";

            yield return null;
        }
    }
}
