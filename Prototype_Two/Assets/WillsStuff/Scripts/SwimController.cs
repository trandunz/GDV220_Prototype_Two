using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwimController : MonoBehaviour
{
    [SerializeField] FastIKFabric Tether;
    [SerializeField] float SwimSpeed = 10.0f;
    [SerializeField] float DragForce = 2.0f;
    [SerializeField] float BoostForce = 30.0f;
    [SerializeField] float BoostCooldown = 1.0f;
    [SerializeField] float DartCooldown = 1.0f;
    [SerializeField] float RotationSpeed = 1000.0f;
    [SerializeField] GameObject WeaponDart;

    [SerializeField] bool PlayerOne = false;

    KeyCode Right = KeyCode.D;
    KeyCode Left = KeyCode.A;
    KeyCode Up = KeyCode.W;
    KeyCode Down = KeyCode.S;
    KeyCode Fire = KeyCode.LeftShift;
    KeyCode Dash = KeyCode.LeftControl;

    GameObject MeshObject;

    bool IsBoosting = false;
    bool IsFiring = false;

    public Transform Origin;
    public Transform finalConnection;

    Vector3 Acceleration = Vector3.zero;
    Vector3 Velocity = Vector3.zero;

    void Start()
    {
        if (PlayerOne)
        {
            Up = (KeyCode)PlayerPrefs.GetInt("P1Up");
            Down = (KeyCode)PlayerPrefs.GetInt("P1Down");
            Left = (KeyCode)PlayerPrefs.GetInt("P1Left");
            Right = (KeyCode)PlayerPrefs.GetInt("P1Right");
            Fire = (KeyCode)PlayerPrefs.GetInt("P1OxyShot");
            Dash = (KeyCode)PlayerPrefs.GetInt("P1OxyBurst");
        }
        else
        {
            Up = (KeyCode)PlayerPrefs.GetInt("P2Up");
            Down = (KeyCode)PlayerPrefs.GetInt("P2Down");
            Left = (KeyCode)PlayerPrefs.GetInt("P2Left");
            Right = (KeyCode)PlayerPrefs.GetInt("P2Right");
            Fire = (KeyCode)PlayerPrefs.GetInt("P2OxyShot");
            Dash = (KeyCode)PlayerPrefs.GetInt("P2OxyBurst");
        }

        MeshObject = GetComponentInChildren<MeshRenderer>().gameObject;
    }
    void Update()
    {
        Drag();
        Boost();
        FireDart();
        Movement();
        RestrictMovement();
        Velocity += Acceleration * Time.fixedDeltaTime;

        transform.position += Velocity * Time.fixedDeltaTime;
        Acceleration = Vector3.zero;

        RotateToVelocity();
    }

    void RestrictMovement()
    {
        float distance = Vector3.Distance(transform.position, Origin.position);
        float ikMaxDistance = Tether.CompleteLength;

        if (distance >= ikMaxDistance)
        {
            Velocity *= -1;
        }
    }

    void FireDart()
    {
        if (Input.GetKeyDown(Fire))
        {
           if (!IsFiring)
            {
                StartCoroutine(FireDartRoutine());
            }
        }
    } 

    void RotateToVelocity()
    {
        if (GetInput().magnitude > 0)
        {
            var dir = Velocity;
            var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            var q = Quaternion.AngleAxis(angle - 90.0f, Vector3.forward);
            MeshObject.transform.rotation = Quaternion.RotateTowards(MeshObject.transform.rotation, q, RotationSpeed * Time.deltaTime);
        }
        else
        {
            var dir = Vector3.up;
            var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            var q = Quaternion.AngleAxis(angle - 90.0f, Vector3.forward);
            MeshObject.transform.rotation = Quaternion.RotateTowards(MeshObject.transform.rotation, q, (RotationSpeed * 0.9f) * Time.deltaTime);
        }
    }

    void ApplyForce(Vector3 _force)
    {
        Acceleration += _force * Time.fixedDeltaTime;
    }

    void Drag()
    {
        ApplyForce(-Velocity * DragForce);
    }
    
    void Movement()
    {
        ApplyForce(GetInput() * SwimSpeed);
    }
    
    void Boost()
    {
        if (Input.GetKeyDown(Dash))
        {
           if (!IsBoosting)
            {
                StartCoroutine(BoostRoutine());
            }
        }
    }

    IEnumerator FireDartRoutine()
    {
        IsFiring = true;

        var dart = Instantiate(WeaponDart, transform.position, Quaternion.identity);
        dart.GetComponent<Dart>().SetDirection(Vector3.down);
        yield return new WaitForSeconds(DartCooldown);
        IsFiring = false;
    }

    IEnumerator BoostRoutine()
    {
        IsBoosting = true;
        Debug.Log("Boost! : " + (GetInput() * BoostForce).magnitude.ToString());
        ApplyForce(GetInput() * BoostForce);
        yield return new WaitForSeconds(BoostCooldown);
        IsBoosting = false;
    }

    Vector3 GetInput()
    {
        Vector3 input = Vector3.zero;
        if (Input.GetKey(Up))
        {
            input.y++;
        }
        if (Input.GetKey(Left))
        {
            input.x--; ;
        }
        if (Input.GetKey(Down))
        {
            input.y--;
        }
        if (Input.GetKey(Right))
        {
            input.x++;
        }
        input.Normalize();
        return input;
    }
}
