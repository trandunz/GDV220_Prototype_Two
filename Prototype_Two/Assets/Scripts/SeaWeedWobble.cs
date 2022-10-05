using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeaWeedWobble : MonoBehaviour
{
    public GameObject[] SeaWeeds;
    public float[] Frequencies;
    public float[] Amplitudes;
    public float[] OriginalPos;

    // Start is called before the first frame update
    void Start()
    {
        Frequencies = new float[SeaWeeds.Length];
        Amplitudes = new float[SeaWeeds.Length];
        OriginalPos = new float[SeaWeeds.Length];

        int i = 0;
        foreach(GameObject seaweed in SeaWeeds)
        {
            Frequencies[i] = Random.Range(1.0f, 2.0f); //speed
            Amplitudes[i] = Random.Range(4.0f, 7.0f);// higher = smaller oscillation
            OriginalPos[i] = seaweed.transform.localScale.z;
            i++;
        }
    }

    // Update is called once per frame
    void Update()
    {
        int i = 0;
        foreach (GameObject seaweed in SeaWeeds)
        {
            Oscillate(seaweed, Frequencies[i], Amplitudes[i], OriginalPos[i]);
            i++;
        }
    }

    private void Oscillate(GameObject seaweed, float freq, float amp, float originalPos)
    {
        float x = Mathf.Sin(freq * Time.time) / amp + originalPos; 
        float y = Mathf.Sin(freq * Time.time) / (100.0f + amp) + originalPos; //seaweed.transform.localScale.y;
        float z = seaweed.transform.localScale.z;
        Debug.Log(x);

        seaweed.transform.localScale = new Vector3(x, y, z);
    }
}
