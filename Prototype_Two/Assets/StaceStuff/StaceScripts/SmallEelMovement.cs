using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallEelMovement : MonoBehaviour
{
    public GameObject Eel1;
    public GameObject Eel2;
    public GameObject Eel3;

    bool eel1Moving = false;
    bool eel2Moving = false;
    bool eel3Moving = false;

    public float eelTimer1 = 0;
    public float eelTimer2 = 0;
    public float eelTimer3 = 0;

    public float MoveSpeed = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        eelTimer1 -= 1 * Time.deltaTime;
        eelTimer2 -= 1 * Time.deltaTime;
        eelTimer3 -= 1 * Time.deltaTime;

        if (eelTimer1 <= 0)
            eel1Moving = true;
        if (eelTimer2 <= 0)
            eel2Moving = true;
        if (eelTimer3 <= 0)
            eel3Moving = true;

        if (eel1Moving == true)
        {
            Eel1.transform.Translate(-MoveSpeed * Time.deltaTime, 0.0f, 0.0f);
        }
        if (eel2Moving == true)
        {
            Eel2.transform.Translate(-MoveSpeed * Time.deltaTime, 0.0f, 0.0f);
        }
        if (eel3Moving == true)
        {
            Eel3.transform.Translate(-MoveSpeed * Time.deltaTime, 0.0f, 0.0f);
        }

    }
}
