using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class changeO2CordColour : MonoBehaviour
{

    [SerializeField] Material material;
    [SerializeField] Color materialDefualtColor;
    [SerializeField] Color fullTension;

    [SerializeField] SwimController[] players;

    // Start is called before the first frame update
    void Start()
    {
        players = FindObjectsOfType<SwimController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (players[0].DistanceFromOrigin + players[1].DistanceFromOrigin >= (players[0].Tether.CompleteLength + players[1].Tether.CompleteLength) * 0.95)
        {
            float percentage = (players[0].Tether.CompleteLength + players[1].Tether.CompleteLength - players[0].DistanceFromOrigin + players[1].DistanceFromOrigin);
            percentage /= ((players[0].DistanceFromOrigin + players[1].DistanceFromOrigin + players[0].Tether.CompleteLength + players[1].Tether.CompleteLength) / 1);
            material.color = Color.Lerp(fullTension, materialDefualtColor, percentage);
        }
        else
        {
            material.color = materialDefualtColor;
        }
    }
}
