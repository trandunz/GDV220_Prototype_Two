using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockToRange : MonoBehaviour
{
    public FastIKFabric maxDistance;
    public float ikMaxDistance;
    public float distance;
    public Transform Origin;
    public Transform finalConnection;
    public Sink sink;

    public Vector3 PreviousPosition;

    public KeyCode Right = KeyCode.D;
    public KeyCode Left = KeyCode.A;
    public KeyCode Up = KeyCode.W;
    public KeyCode Down = KeyCode.S;

    Vector3 move;

    private void Start()
    {
        sink = GetComponent<Sink>();
        sink.ison = false;
    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector3.Distance(transform.position, Origin.position);
        ikMaxDistance = maxDistance.CompleteLength;

        if (distance >= ikMaxDistance)
        {
            if (!sink.ison)
            {
                transform.position = PreviousPosition;
            }
            sink.ison = true;
            if (distance >= ikMaxDistance + 0.1f)
            {
                transform.position = new Vector3(Origin.position.x, Origin.position.y, transform.position.z);
            }
        }
        else
        {
            sink.ison = false;
        }

        if (Input.GetKey(Right) && Vector3.Distance((move = new Vector3(transform.position.x + (5 * Time.deltaTime), transform.position.y, transform.position.z)), Origin.position) < ikMaxDistance)
        {
            transform.position = move;
        }

        if (Input.GetKey(Left) && Vector3.Distance((move = new Vector3(transform.position.x - (5 * Time.deltaTime), transform.position.y, transform.position.z)), Origin.position) < ikMaxDistance)
        {
            transform.position = move;
        }

        if (Input.GetKey(Up) && Vector3.Distance((move = new Vector3(transform.position.x, transform.position.y + (5 * Time.deltaTime), transform.position.z)), Origin.position) < ikMaxDistance)
        {
            transform.position = move;
        }

        if (Input.GetKey(Down) && Vector3.Distance((move = new Vector3(transform.position.x, transform.position.y - (5 * Time.deltaTime), transform.position.z)), Origin.position) < ikMaxDistance)
        {
            transform.position = move;
        }

        PreviousPosition = transform.position;
    }
}
