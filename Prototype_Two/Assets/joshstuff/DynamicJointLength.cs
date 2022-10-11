using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicJointLength : MonoBehaviour
{
    [SerializeField] FastIKFabric player1;
    [SerializeField] FastIKFabric player2;

    [SerializeField] ConfigurableJoint player1Joint;
    [SerializeField] ConfigurableJoint player2Joint;
    public static float reduction = 0.2f;
    // Start is called before the first frame update
    void Start()
    {
        SoftJointLimit limit1 = new SoftJointLimit();
        limit1.limit = player1.CompleteLength;
        player1Joint.linearLimit = limit1;

        SoftJointLimit limit2 = new SoftJointLimit();
        limit2.limit = player2.CompleteLength;
        player2Joint.linearLimit = limit2;
    }

    private void FixedUpdate()
    {
        SoftJointLimit limit1 = new SoftJointLimit();
        limit1.limit = player1.CompleteLength;
        player1Joint.linearLimit = limit1;

        SoftJointLimit limit2 = new SoftJointLimit();
        limit2.limit = player2.CompleteLength;
        player2Joint.linearLimit = limit2;
    }
}
