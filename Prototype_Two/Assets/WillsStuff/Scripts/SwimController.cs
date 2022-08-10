using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwimController : MonoBehaviour
{
    public FastIKFabric Tether;
    [SerializeField] float SwimSpeed = 10.0f;
    [SerializeField] float DragForce = 2.0f;
    [SerializeField] float BoostForce = 30.0f;
    [SerializeField] float BoostCooldown = 1.0f;
    [SerializeField] float RotationSpeed = 1000.0f;

    [SerializeField] bool PlayerOne = false;

    [SerializeField] GameObject MeshObject;

    private float FixedDeltaTime;

    OxygenTankValue oxygenTank;

    KeyCode Right = KeyCode.D;
    KeyCode Left = KeyCode.A;
    KeyCode Up = KeyCode.W;
    KeyCode Down = KeyCode.S;
    KeyCode Dash = KeyCode.LeftControl;

    Animator animator;
    SkinnedMeshRenderer Mesh;
    SwimController otherPlayer = null;
    IKInitialiser cord;

    bool IsBoosting = false;
    bool IsOnLeftSize = false;
    bool IsInvulnrable = false;
    bool IsComingOutOfInvulnrable = false;

    public float DistanceFromOrigin = 0.0f;

    public Transform Origin;
    public Transform finalConnection;

    Vector3 Acceleration = Vector3.zero;
    Vector3 Velocity = Vector3.zero;

    [SerializeField] Material YellowMaterial;

    [Header("Audio")]
    public GameObject audioDash;
    public GameObject audioShoot;
    public GameObject audioHurt;
    public GameObject audioDead;
    public GameObject audioOxygem;
    public GameObject audioBubble;

    // screen shake
    private Shake shake;

    // bubbles
    public GameObject Bubbles;
    public Transform BubblesPosition;
    public Quaternion BubblesRotation;

    void Start()
    {
        animator = GetComponentInChildren<Animator>();

        FixedDeltaTime = Time.fixedDeltaTime;

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
            Mesh.material = YellowMaterial;

        // screen shake
        shake = GameObject.FindGameObjectWithTag("ScreenShake").GetComponent<Shake>();

        BubblesRotation = BubblesPosition.rotation;
    }

    void Update()
    {
        if (PlayerOne)
        {
            Up = (KeyCode)PlayerPrefs.GetInt("P1Up");
            Down = (KeyCode)PlayerPrefs.GetInt("P1Down");
            Left = (KeyCode)PlayerPrefs.GetInt("P1Left");
            Right = (KeyCode)PlayerPrefs.GetInt("P1Right");
            Dash = (KeyCode)PlayerPrefs.GetInt("P1OxyBurst");
        }
        else
        {
            Up = (KeyCode)PlayerPrefs.GetInt("P2Up");
            Down = (KeyCode)PlayerPrefs.GetInt("P2Down");
            Left = (KeyCode)PlayerPrefs.GetInt("P2Left");
            Right = (KeyCode)PlayerPrefs.GetInt("P2Right");
            Dash = (KeyCode)PlayerPrefs.GetInt("P2OxyBurst");
        }
        RotateToVelocity();
        Boost();
        HandleAnimations();
    }

    void FixedUpdate()
    {
        Time.fixedDeltaTime = FixedDeltaTime * Time.timeScale;
        
        Drag();
        RestrictMovement();
        Movement();

        Velocity += Acceleration * Time.fixedDeltaTime;
        transform.position += Velocity * Time.fixedDeltaTime;
        Acceleration = Vector3.zero;
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
            MeshObject.transform.rotation = Quaternion.RotateTowards(MeshObject.transform.rotation, q, RotationSpeed * Time.deltaTime);
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag is "Oxygem")
        {
            Destroy(Instantiate(audioOxygem), 2.0f);
            Destroy(other.gameObject);
            GemManager.instance.AddGems(1);
        }
        if (other.gameObject.tag is "Bubble")
        {
            oxygenTank.AddOxygen(0.2f);
            Destroy(other.gameObject);
            Destroy(Instantiate(audioBubble), 3.0f);
        }
        if (other.gameObject.tag is "Enemy")
        {
            if (!IsInvulnrable)
            {
                Destroy(Instantiate(Bubbles, BubblesPosition.position, BubblesRotation, gameObject.transform), 5.0f);
                Destroy(Instantiate(audioHurt), 2.0f);
                oxygenTank.DamageOxygenUse();
                Debug.Log("Player Got Hit!");
                StartCoroutine(StartInvulnrability());
                shake.CamShake();
            }
        }
    }

    void HandleAnimations()
    {
        animator.SetBool("IsMoving", GetInput().magnitude > 0);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag is "Enemy")
        {
            IsInvulnrable = true;
            IsComingOutOfInvulnrable = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag is "Enemy")
        {
            if (IsInvulnrable && !IsComingOutOfInvulnrable)
            {
                StartCoroutine(RemoveInvulnrability());
            }
        }
    }

    IEnumerator StartInvulnrability()
    {
        IsInvulnrable = true;
        IsComingOutOfInvulnrable = false;
        float FlashSpeed = 0.25f;
        float FlashTimer = FlashSpeed;
        Color originalColor = Mesh.material.color;
        while (IsInvulnrable)
        {
            FlashTimer -= Time.deltaTime;
            if (FlashTimer <= 0)
            {
                FlashTimer = FlashSpeed;
                if (Mesh.material.color == originalColor)
                {
                    Mesh.material.color = Color.red;
                }
                else
                {
                    Mesh.material.color = originalColor;
                }
            }

            yield return new WaitForEndOfFrame();
        }
        Mesh.material.color = originalColor;
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
