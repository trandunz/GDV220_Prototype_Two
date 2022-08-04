using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelLoader : MonoBehaviour
{
    public void LoadLevel(int _scene)
    {
        StartCoroutine(LoadAsync(_scene));
    }

    IEnumerator LoadAsync(int _scene)
    {
        DontDestroyOnLoad(gameObject);
        SceneManager.LoadScene(3);

        yield return new WaitForSeconds(0.2f);

        Slider progressSlider = FindObjectOfType<Slider>();
        TMPro.TextMeshProUGUI progressText = FindObjectOfType<TMPro.TextMeshProUGUI>();

        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(_scene);

        while(!asyncOperation.isDone)
        {
            float progress = Mathf.Clamp01(asyncOperation.progress / 0.9f);
            progressSlider.value = progress;
            progressText.text = (progress * 100).ToString() + "%";

            yield return null;
        }

        Destroy(gameObject);
    }
}
