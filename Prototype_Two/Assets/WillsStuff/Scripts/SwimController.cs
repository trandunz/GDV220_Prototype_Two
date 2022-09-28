using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwimController : MonoBehaviour
{
    KeyCode Right = KeyCode.D;
    KeyCode Left = KeyCode.A;
    KeyCode Up = KeyCode.W;
    KeyCode Down = KeyCode.S;
    KeyCode Dash = KeyCode.LeftControl;

    [Header("Swim Settings")]
    [SerializeField] float SwimSpeed = 10.0f;
    [SerializeField] float DragForce = 2.0f;
    [SerializeField] float BoostForce = 30.0f;
    [SerializeField] float BoostCooldown = 1.0f;
    [SerializeField] float RotationSpeed = 1000.0f;
    [SerializeField] GameObject PointsPopup;

    [Header("Player Settings")]
    [SerializeField] bool PlayerOne = false;
    [SerializeField] GameObject MeshObject;
    bool CanMove = true;
    bool IsBoosting = false;
    bool IsOnLeftSize = false;
    bool IsInvulnrable = false;
    bool IsComingOutOfInvulnrable = false;

    DepthPanel ScoreScript;

    SwimController otherPlayer = null;

    OxygenTankValue oxygenTank;

    Animator animator;
    SkinnedMeshRenderer Mesh;

    Rigidbody m_RigidBody;

    IKInitialiser cord;
    public FastIKFabric Tether;
    public float DistanceFromOrigin = 0.0f;
    public Transform Origin;
    public Transform finalConnection;

    [Header("Materials")]
    [SerializeField] Material YellowMaterial;
    [SerializeField] Material RedMaterial;

    [Header("Audio")]
    public GameObject audioDash;
    public GameObject audioShoot;
    public GameObject audioHurt;
    public GameObject audioDead;
    public GameObject audioOxygem;
    public GameObject audioBubble;

    [Header("Particles")]
    // screen shake
    private Shake shake;

    // bubbles
    public GameObject Bubbles;
    public Transform BubblesPosition;
    public Quaternion BubblesRotation;

    void Start()
    {
        animator = GetComponentInChildren<Animator>();

        m_RigidBody = GetComponent<Rigidbody>();

        oxygenTank = FindObjectOfType<OxygenTankValue>();

        Mesh = GetComponentInChildren<SkinnedMeshRenderer>();

        if (transform.position.x > Tether.transform.position.x)
        {
            IsOnLeftSize = true;
        }

        cord = FindObjectOfType<IKInitialiser>();
        foreach (SwimController player in FindObjectsOfType<SwimController>())
        {
            if (player != this)
            {
                otherPlayer = player;
            }
        }

        if (PlayerOne)
        {
            Mesh.material = YellowMaterial;
        }
        else
        {
            Mesh.material = RedMaterial;
        }

        // screen shake
        shake = GameObject.FindGameObjectWithTag("ScreenShake").GetComponent<Shake>();

        BubblesRotation = BubblesPosition.rotation;

        ScoreScript = FindObjectOfType<DepthPanel>();
    }

    void Update()
    {
        if (PlayerOne)
        {
            Up = (KeyCode.W);
            Down = (KeyCode.S);
            Left = (KeyCode.A);
            Right = (KeyCode.D);
            Dash = (KeyCode.T);
        }
        else
        {
            Up = (KeyCode.UpArrow);
            Down = (KeyCode.DownArrow);
            Left = (KeyCode.LeftArrow);
            Right = (KeyCode.RightArrow);
            Dash = (KeyCode.V);
        }
        RotateToVelocity();
        Boost();
        HandleAnimations();
    }

    void FixedUpdate()
    {
        Drag();
        RestrictMovement();
        Movement();
    }

    void RestrictMovement()
    {
        DistanceFromOrigin = Vector3.Distance(transform.position, Origin.position);
        float tetherLength = Tether.CompleteLength;
        float totalTetherLength = otherPlayer.Tether.CompleteLength + tetherLength;
        int minTetherLength = cord.MinChainLength;

        if (DistanceFromOrigin >= tetherLength)
        {
            if (GetInput().magnitude == 0)
            {
                ApplyForce(new Vector3(Origin.position.x - transform.position.x, Origin.position.y - transform.position.y, 0).normalized * SwimSpeed * 3.0f);
            }
            else
            {
                if (IsOnLeftSize)
                {
                    if (otherPlayer?.Tether.Bones.Length > minTetherLength)
                    {
                        if (DistanceFromOrigin + otherPlayer?.DistanceFromOrigin >= totalTetherLength + 1.0f)
                        {
                            ApplyForce(new Vector3(Origin.position.x - transform.position.x, Origin.position.y - transform.position.y, 0).normalized * SwimSpeed * 3.0f);
                        }
                        else
                        {
                            cord.moveleft = true;
                        }
                        
                    }
                    else
                    {
                        ApplyForce(new Vector3(Origin.position.x - transform.position.x, Origin.position.y - transform.position.y, 0).normalized * SwimSpeed * 3.0f);
                    }
                }
                else
                {
                    if (otherPlayer?.Tether.Bones.Length > minTetherLength)
                    {
                        if (DistanceFromOrigin + otherPlayer?.DistanceFromOrigin >= totalTetherLength + 1.0f)
                        {
                            ApplyForce(new Vector3(Origin.position.x - transform.position.x, Origin.position.y - transform.position.y, 0).normalized * SwimSpeed * 3.0f);
                        }
                        else
                        {
                            cord.moveright = true;
                        }
                    }
                    else
                    {
                        ApplyForce(new Vector3(Origin.position.x - transform.position.x, Origin.position.y - transform.position.y, 0).normalized * SwimSpeed * 3.0f);
                    }
                }
            }
        }
    }

    void RotateToVelocity()
    {
        if (GetInput().magnitude > 0)
        {
            var dir = GetInput().normalized;
            var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            var q = Quaternion.AngleAxis(angle - 90, Vector3.forward);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, q, RotationSpeed * Time.deltaTime);
        }
    }

    void ApplyForce(Vector3 _force)
    {
        m_RigidBody.AddForce(_force * Time.fixedDeltaTime, ForceMode.Acceleration);
    }

    void Drag()
    {
        ApplyForce(-m_RigidBody.velocity * DragForce);
    }
    
    void Movement()
    {
        ApplyForce(GetInput() * SwimSpeed);
    }
    
    void Boost()
    {
        if (Input.GetKeyDown(Dash) && CanMove)
        {
           if (!IsBoosting)
           {
                Destroy(Instantiate(audioDash), 2.0f);
                StartCoroutine(BoostRoutine());
                
                Destroy(Instantiate(Bubbles, BubblesPosition.position, BubblesRotation, gameObject.transform),5.0f);

            }
        }
    }

    IEnumerator BoostRoutine()
    {
        oxygenTank.DashOxygenUse();
        float distance = Vector3.Distance(transform.position, Origin.position);
        float ikMaxDistance = Tether.CompleteLength;
        if (distance < ikMaxDistance)
        {
            IsBoosting = true;
            Debug.Log("Boost! : " + (GetInput() * BoostForce).magnitude.ToString());
            ApplyForce(GetInput() * BoostForce);
            yield return new WaitForSeconds(BoostCooldown);
            IsBoosting = false;
        }
        yield return null;
    }

    void HandleAnimations()
    {
        animator.SetBool("IsMoving", GetInput().magnitude > 0);
    }

    public void Die()
    {
        animator.SetBool("Dead", true);
        CanMove = false;
    }

    public void PickupOxygem(Collider other)
    {
        Destroy(Instantiate(audioOxygem), 2.0f);
        Destroy(other.gameObject);
        oxygenTank.AddOxygem();
    }

    public void PickupBubble(Collider other)
    {
        oxygenTank.AddOxygen(0.2f);
        Destroy(other.gameObject);
        Destroy(Instantiate(audioBubble), 3.0f);
    }

    public void HitEnemy()
    {
        if (!IsInvulnrable)
        {
            Debug.Log("Player Got Hit!");

            IsInvulnrable = true;

            Destroy(Instantiate(Bubbles, BubblesPosition.position, BubblesRotation, gameObject.transform), 5.0f);
            Destroy(Instantiate(audioHurt), 2.0f);
            oxygenTank.DamageOxygenUse();
            StartCoroutine(StartInvulnrability());
            shake.CamShake();
        }

        IsComingOutOfInvulnrable = false;
    }

    public void LeaveEnemy()
    {
        if (IsInvulnrable && !IsComingOutOfInvulnrable)
        {
            StartCoroutine(RemoveInvulnrability());
        }
    }

    IEnumerator StartInvulnrability()
    {
        IsInvulnrable = true;
        IsComingOutOfInvulnrable = false;
        float FlashSpeed = 0.25f;
        float FlashTimer = FlashSpeed;
        Material originalMaterial = Mesh.material;
        Material redMat = Mesh.material;
        redMat.color = Color.red;
        while (IsInvulnrable)
        {
            FlashTimer -= Time.deltaTime;
            if (FlashTimer <= 0)
            {
                FlashTimer = FlashSpeed;
                if (Mesh.material == originalMaterial)
                {
                    Mesh.material = redMat;
                }
                else
                {
                    Mesh.material = originalMaterial;
                }
            }

            yield return new WaitForEndOfFrame();
        }
        Mesh.material = originalMaterial;
    }

    IEnumerator RemoveInvulnrability()
    {
        IsComingOutOfInvulnrable = true;
        yield return new WaitForSeconds(0.4f);
        if (IsComingOutOfInvulnrable == true)
        {
            IsInvulnrable = false;
            Debug.Log("Player Ready for more.");
            IsComingOutOfInvulnrable = false;
        }  
    }

    Vector3 GetInput()
    {
        Vector3 input = Vector3.zero;
        if (Input.GetKey(Up) && CanMove)
        {
            input.y++;
        }
        if (Input.GetKey(Left) && CanMove)
        {
            input.x--; ;
        }
        if (Input.GetKey(Down) && CanMove)
        {
            input.y--;
        }
        if (Input.GetKey(Right) && CanMove)
        {
            input.x++;
        }
        input.Normalize();

        return input;
    }
}
