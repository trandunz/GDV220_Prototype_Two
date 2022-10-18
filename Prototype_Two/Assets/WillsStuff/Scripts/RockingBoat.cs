using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockingBoat : MonoBehaviour
{
    [SerializeField] float rockspeed;
    Vector3 StartPosition;
    Vector3 StartRotation;
    [SerializeField] float xAmplitude = 2.0f;
    [SerializeField] float zAmplitude = 3.0f;
    [SerializeField] float yAmplitude = 0.1f;
    void Start()
    {
        StartPosition = transform.position;
        StartRotation = transform.rotation.eulerAngles;
    }
    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.Euler(Mathf.Sin(Time.time * rockspeed) * xAmplitude, StartRotation.y, Mathf.Cos(Time.time * rockspeed) * zAmplitude);

        transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, StartPosition.y + Mathf.Cos(Time.time * rockspeed) * yAmplitude, transform.position.z), Time.deltaTime * 5.0f);
    }

    public void Splash()
    {
        transform.position = StartPosition + Vector3.down * 0.2f;
    }
}
