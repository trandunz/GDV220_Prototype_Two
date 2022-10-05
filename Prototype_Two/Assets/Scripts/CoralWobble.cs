using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoralWobble : MonoBehaviour
{
    public GameObject[] CoralPipes;
    public float[] Frequencies;
    public float[] Amplitudes;
    public float[] OriginalPos;

    // Start is called before the first frame update
    void Start()
    {
        Frequencies = new float[CoralPipes.Length];
        Amplitudes = new float[CoralPipes.Length];
        OriginalPos = new float[CoralPipes.Length];

        int i = 0;
        foreach (GameObject coral in CoralPipes)
        {
            Frequencies[i] = Random.Range(0.5f, 1.5f);
            Amplitudes[i] = Random.Range(7.0f, 10.0f); // higher = smaller oscillation
            OriginalPos[i] = coral.transform.localScale.z;
            i++;
        }
    }

    // Update is called once per frame
    void Update()
    {
        int i = 0;
        foreach (GameObject coral in CoralPipes)
        {
            Oscillate(coral, Frequencies[i], Amplitudes[i], OriginalPos[i]);
            i++;
        }
    }

    private void Oscillate(GameObject coral, float freq, float amp, float originalPos)
    {
        float x = Mathf.Sin(freq * Time.time) / amp + originalPos;
        float y = coral.transform.localScale.y;
        float z = Mathf.Sin(freq * Time.time) / (100.0f + amp) + originalPos; //coral.transform.localScale.z;
        Debug.Log(x);

        coral.transform.localScale = new Vector3(x, y, z);
    }
}
