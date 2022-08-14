using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

// tutorial https://www.youtube.com/watch?v=qqOAzn05fvk

public class FastIKFabric : MonoBehaviour
{
    public int ChainLength = 2;

    public Transform deletethis;

    public Transform Target;
    public Transform Pole;

    [Header("Solver Parameters")]
    public int Iterations = 10;

    public float Delta = 0.001f;

    [Range(0, 1)]
    public float SnapBackStrength = 1f;

    public float[] BonesLength;
    public float CompleteLength;
    public Transform[] Bones;
    public Vector3[] Positions;
    public Vector3[] StartDirectionSucc;
    public Quaternion[] StartRotationBone;
    public Quaternion StartRotationTarget;
    public Quaternion StartRotationRoot;

    // Start is called before the first frame update
    void Awake()
    {
        StartRotationTarget = Target.rotation;
        Init();
    }

    public void AttachNewSphere(GameObject _obj, float _distance)
    {
        _obj.transform.parent = Bones[Bones.Length - 2];
        _obj.transform.localPosition = new Vector3(_distance, 0.0f, 0.0f);
        Transform[] bones2 = new Transform[Bones.Length + 1];
        for (int i = 0; i < Bones.Length; i++)
        {
            bones2[i] = Bones[i];
        }
        bones2[bones2.Length - 2] = _obj.transform;
        bones2[bones2.Length - 1] = Bones[Bones.Length - 1];
        Bones = bones2;
        Bones[Bones.Length - 1].parent = _obj.transform;
        Bones[Bones.Length - 1].localPosition = new Vector3(_distance, 0.0f, 0.0f);        
        Init();
    }

    void Init()
    {
        ChainLength = Bones.Length - 1;
        Positions = new Vector3[ChainLength + 1];
        BonesLength = new float[ChainLength];

        StartDirectionSucc = new Vector3[ChainLength + 1];
        StartRotationBone = new Quaternion[ChainLength + 1];

        CompleteLength = 0;

        var current = transform;
        for (var i = Bones.Length - 1; i >= 0; i--)
        {
            Bones[i] = current;
            StartRotationBone[i] = current.rotation;
            if (i == Bones.Length - 1)
            {
                StartDirectionSucc[i] = Target.position - current.position;
            }
            else
            {
                StartDirectionSucc[i] = Bones[i + 1].position - current.position;
                BonesLength[i] = StartDirectionSucc[i].magnitude;
                CompleteLength += BonesLength[i];
            }
            current = current.parent;
        }
    }

    public void RemomveSphere(float _distance)
    {
        Transform[] Bones2 = new Transform[Bones.Length - 1];
        for (int i = 0; i < Bones2.Length; i++)
        {
            Bones2[i] = Bones[i];
        }

        Bones[Bones.Length - 1].parent = Bones2[Bones2.Length - 2];

        deletethis = Bones2[Bones2.Length - 1];
        Bones2[Bones2.Length - 1] = Bones[Bones.Length - 1];

        Destroy(deletethis.gameObject);
        Bones = Bones2;
        Bones[Bones.Length - 1].localPosition = new Vector3(_distance, 0.0f, 0.0f);

        Init();
    }

    private void LateUpdate()
    {
        ResolveIK();
    }

    void ResolveIK()
    {
        if (Target == null)
            return;

        if (BonesLength.Length != ChainLength)
            Init();

        for (int i = 0; i < Bones.Length; i++)
        {
            Positions[i] = Bones[i].position;
        }

        var RootRot = (Bones[0].parent != null) ? Bones[0].parent.rotation : Quaternion.identity;
        var RootRotDif = RootRot * Quaternion.Inverse(StartRotationRoot);

        // calculations
        if ((Target.position - Bones[0].position).sqrMagnitude >= CompleteLength * CompleteLength)
        {
            var direction = (Target.position - Positions[0]).normalized;

            for (int i = 1; i < Positions.Length; i++)
            {
                Positions[i] = Positions[i - 1] + direction * BonesLength[i-1];
            }
        }
        else
        {
            for (int iteration = 0; iteration < Iterations; iteration++)
            {
                // back
                for (int i = Positions.Length - 1; i > 0; i--)
                {
                    if (i == Positions.Length - 1)
                    {
                        Positions[i] = Target.position;
                    }
                    else
                    {
                        Positions[i] = Positions[i + 1] + (Positions[i] - Positions[i + 1]).normalized * BonesLength[i];
                    }
                }

                // foward
                for (int i = 1; i < Positions.Length; i++)
                {
                    Positions[i] = Positions[i - 1] + (Positions[i] - Positions[i - 1]).normalized * BonesLength[i - 1];
                }

                if ((Positions[Positions.Length - 1] - Target.position).sqrMagnitude < Delta * Delta)
                    break;
            }
        }

        // pole
        if (Pole != null)
        {
            for (int i = 1; i < Positions.Length - 1; i++)
            {

                var plane = new Plane(Positions[i + 1] - Positions[i - 1], Positions[i - 1]);
                var projectedPole = plane.ClosestPointOnPlane(Pole.position);
                var projectedBone = plane.ClosestPointOnPlane(Positions[i]);
                var angle = Vector3.SignedAngle(projectedBone - Positions[i - 1], projectedPole - Positions[i - 1], plane.normal);
                Positions[i] = Quaternion.AngleAxis(angle, plane.normal) * (Positions[i] - Positions[i - 1]) + Positions[i - 1];
            }
        }

        for (int i = 0; i < Positions.Length; i++)
        {
            if (i == Positions.Length - 1)
            {
                Bones[i].rotation = Target.rotation * Quaternion.Inverse(StartRotationTarget) * StartRotationBone[i];
            }
            else
            {
                Bones[i].rotation = Quaternion.FromToRotation(StartDirectionSucc[i], Positions[i + 1] - Positions[i]) * StartRotationBone[i];
            }

            Bones[i].position = Positions[i];
        }
        ClampScale();
    }

    private void OnDrawGizmos()
    {
        /*var current = transform;
        for (int i = 0; i < ChainLength && current != null && current.parent != null; i++)
        {
            var scale = Vector3.Distance(current.position, current.parent.position) * 0.1f;
            Handles.matrix = Matrix4x4.TRS(current.position, Quaternion.FromToRotation(Vector3.up, current.parent.position - current.position), 
                new Vector3(scale, Vector3.Distance(current.parent.position, current.position), scale));
            Handles.color = Color.green;
            Handles.DrawWireCube(Vector3.up * 0.5f, Vector3.one);
            current = current.parent;
        }*/
    }

    private void ClampScale()
    {
        Vector3 scale = new Vector3(0.3f, 0.3f, 0.3f);
        Bones[0].localScale = scale;
    }
}
