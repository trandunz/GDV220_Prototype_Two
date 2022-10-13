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
    [SerializeField] float SlowSpeed = 10.0f;
    [SerializeField] float DragForce = 2.0f;
    [SerializeField] float RotationSpeed = 1000.0f;
    [SerializeField] GameObject PointsPopup;
    float MoveSpeed = 0.0f;
    float SlowTimer = 0.0f;
    [SerializeField] float SlowTime = 0.2f;

    [Header("Dash Settings")]
    [SerializeField] float BoostForce = 30.0f;
    [SerializeField] float BoostCooldown = 1.0f;
    [SerializeField] float DashingTime;
    [SerializeField] float PostDashBoost;
    bool IsBoosting = false;
    bool canDash = true;
    float RemainingBoostTime;

    [Header("Player Settings")]
    [SerializeField] bool PlayerOne = false;
    [SerializeField] GameObject MeshObject;
    [SerializeField] float m_InvulnrabilityTime = 0.4f;
    bool CanMove = true;
    bool IsOnLeftSize = false;
    bool IsInvulnrable = false;
    float m_InvulnrabilityTimer;

    BubbleBuffUI[] m_BubbleBuffUIs;

    [Header("GUI Settings")]
    BubbleBuffPanel BubbleBuffUI;
    DepthPanel ScoreScript;

    SwimController otherPlayer = null;

    OxygenTankValue oxygenTank;

    Animator animator;
    SkinnedMeshRenderer Mesh;

    Rigidbody m_RigidBody;

    [Header("Bubble Buff Settings")]
    [SerializeField] float MagnetRange = 30.0f;
    [SerializeField] float MagnetStrength = 30.0f;
    [SerializeField] float BubbleBuffUseTime = 10.0f;
    [SerializeField] GameObject ShieldBubble = null;
    [SerializeField] GameObject GlowStick = null;
    GameObject m_ActivePowerup = null;
    BubbleBuff.BUFFTYPE m_CurrentBubbleBuff = BubbleBuff.BUFFTYPE.UNASSIGNED;
    public bool IsUsingBubbleBuff = false;

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
    private ScreenShake shake;

    // bubbles
    public GameObject Bubbles;
    public Transform BubblesPosition;
    public Quaternion BubblesRotation;

    [Header("Paralyze Settings")]
    [SerializeField] float m_ParalyzeDuration;
    float m_ParalyzeTimer;

    [Header("Line Renderer")]
    public LineRenderer StretchyLine;
    public GameObject Player1Cord; // to get cord position
    public GameObject Player2Cord; // to get cord position


    void Start()
    {
        m_BubbleBuffUIs = FindObjectsOfType<BubbleBuffUI>();

        MoveSpeed = SwimSpeed;

        BubbleBuffUI = FindObjectOfType<BubbleBuffPanel>();

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
        shake = GameObject.FindObjectOfType<ScreenShake>();

        BubblesRotation = BubblesPosition.rotation;

        ScoreScript = FindObjectOfType<DepthPanel>();

        if (PlayerOne)
        {
            Up = (KeyCode.W);
            Down = (KeyCode.S);
            Left = (KeyCode.A);
            Right = (KeyCode.D);
            Dash = (KeyCode.V);
        }
        else
        {
            Up = (KeyCode.UpArrow);
            Down = (KeyCode.DownArrow);
            Left = (KeyCode.LeftArrow);
            Right = (KeyCode.RightArrow);
            Dash = (KeyCode.Keypad1);
        }
    }

    void Update()
    {
        if (SlowTimer > 0)
            SlowTimer -= Time.deltaTime;
        else
        {
            MoveSpeed = SwimSpeed;
        }

        if (m_InvulnrabilityTimer > 0)
            m_InvulnrabilityTimer -= Time.deltaTime;
        if (IsInvulnrable && m_InvulnrabilityTimer <= 0)
            RemoveInvulnrability();

        RotateToVelocity();

        if (Input.GetKeyDown(Dash))
        {
            if (m_CurrentBubbleBuff != BubbleBuff.BUFFTYPE.UNASSIGNED)
            {
                UseBubbleBuff();
            }
            else
            {
                Boost();
            }
        }

        
        HandleAnimations();

        if (!IsUsingBubbleBuff)
        {
            SetAvailableBuffUI();
        }

        if (Input.GetKeyDown(KeyCode.Keypad9))
        {
            PickupBubbleBuff(BubbleBuff.BUFFTYPE.GEMCHEST);
        }
    }

    void FixedUpdate()
    {
        Drag();
        RestrictMovement();
        Movement();
        UpdateStretchyLine();
    }

    void RestrictMovement()
    {
        DistanceFromOrigin = Vector3.Distance(transform.position, Origin.position);
        float tetherLength = Tether.CompleteLength;
        float totalTetherLength = otherPlayer.Tether.CompleteLength + tetherLength;
        int minTetherLength = cord.MinChainLength;

        if (DistanceFromOrigin >= tetherLength - DynamicJointLength.reduction)
        {
            if (GetInput().magnitude == 0)
            {
                ApplyForce(new Vector3(Origin.position.x - transform.position.x, Origin.position.y - transform.position.y, 0).normalized * MoveSpeed * 3.0f);
            }
            if (IsOnLeftSize)
            {
                if (otherPlayer?.Tether.Bones.Length > minTetherLength)
                {
                    if (DistanceFromOrigin + otherPlayer?.DistanceFromOrigin >= totalTetherLength - DynamicJointLength.reduction && !IsBoosting)
                    {
                        ApplyForce(new Vector3(Origin.position.x - transform.position.x, Origin.position.y - transform.position.y, 0).normalized * MoveSpeed * 3.0f);
                    }
                    else
                    {
                        cord.moveleft = true;
                    }

                }
                else
                {
                    ApplyForce(new Vector3(Origin.position.x - transform.position.x, Origin.position.y - transform.position.y, 0).normalized * MoveSpeed * 3.0f);
                    ApplyForce(new Vector3(Origin.position.x - transform.position.x, Origin.position.y - transform.position.y, 0).normalized * MoveSpeed * 3.0f);
                }
            }
            else
            {
                if (otherPlayer?.Tether.Bones.Length > minTetherLength)
                {
                    if (DistanceFromOrigin + otherPlayer?.DistanceFromOrigin >= totalTetherLength - DynamicJointLength.reduction && !IsBoosting)
                    {
                        ApplyForce(new Vector3(Origin.position.x - transform.position.x, Origin.position.y - transform.position.y, 0).normalized * MoveSpeed * 3.0f);
                    }
                    else
                    {
                        cord.moveright = true;
                    }
                }
                else
                {
                    ApplyForce(new Vector3(Origin.position.x - transform.position.x, Origin.position.y - transform.position.y, 0).normalized * MoveSpeed * 3.0f);
                }
            }
        }
    }

    public void Slow()
    {
        MoveSpeed = SlowSpeed;
        SlowTimer = SlowTime;
    }

    void UseBubbleBuff()
    {
        if (!IsUsingBubbleBuff)
        {
            StartCoroutine(BubbleBuffRoutine());
        }
    }

    public void Paralyze()
    {
        if (m_ParalyzeTimer <= 0)
        {
            StartCoroutine(ParalyzeRoutine());
        }
        else
        {
            m_ParalyzeTimer = m_ParalyzeDuration;
        }
    }

    IEnumerator ParalyzeRoutine()
    {
        CanMove = false;
        m_ParalyzeTimer = m_ParalyzeDuration;
        while (m_ParalyzeTimer > 0)
        {
            m_ParalyzeTimer -= Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        if (m_ActivePowerup)
        {
            IsUsingBubbleBuff = false;
            Destroy(m_ActivePowerup);
        }
        CanMove = true;
    }

    IEnumerator BubbleBuffRoutine()
    {
        IsUsingBubbleBuff = true;
        bool usedOxyChest = false;
        bool usedFlare = false;

        float bubbleBuffUseTimer = BubbleBuffUseTime;
        while (bubbleBuffUseTimer > 0 && IsUsingBubbleBuff)
        {
            SetAvailableBuffUI();

            switch (m_CurrentBubbleBuff)
            {
                case BubbleBuff.BUFFTYPE.RANDOM:
                    {
                        m_CurrentBubbleBuff = (BubbleBuff.BUFFTYPE)Random.Range(2, 6);

                        break;
                    }
                case BubbleBuff.BUFFTYPE.FLARE:
                    {
                        SetBuffCooldownWidget(bubbleBuffUseTimer / 0.8f);
                        if (!usedFlare)
                        {
                            usedFlare = true;
                            Vector3 glowStickPos = transform.position;
                            glowStickPos.z = -2.0f;
                            Instantiate(GlowStick, glowStickPos, Quaternion.identity);
                            bubbleBuffUseTimer = 0.8f;
                        }

                        break;
                    }
                case BubbleBuff.BUFFTYPE.GEMCHEST:
                    {
                        SetBuffCooldownWidget(bubbleBuffUseTimer / 0.8f);
                        if (!usedOxyChest)
                        {
                            usedOxyChest = true;
                            bubbleBuffUseTimer = 0.8f;
                            if (PlayerOne)
                            {
                                StartCoroutine(m_BubbleBuffUIs[0].SpawnGemChestGemsRoutine());
                            }
                            else
                            {
                                StartCoroutine(m_BubbleBuffUIs[1].SpawnGemChestGemsRoutine());
                            }
                        }

                        break;
                    }
                case BubbleBuff.BUFFTYPE.MAGNET:
                    {
                        SetBuffCooldownWidget(bubbleBuffUseTimer / BubbleBuffUseTime);
                        foreach (Oxygem oxygem in FindObjectsOfType<Oxygem>())
                        {
                            if (Vector3.Distance(oxygem.transform.position, transform.position) <= MagnetRange)
                            {
                                oxygem.transform.position += (transform.position - oxygem.transform.position) * Time.deltaTime * MagnetStrength;
                            }
                        }
                        break;
                    }
                case BubbleBuff.BUFFTYPE.SHIELD:
                    {
                        SetBuffCooldownWidget(bubbleBuffUseTimer / BubbleBuffUseTime);
                        if (m_ActivePowerup == null)
                        {
                            m_ActivePowerup = Instantiate(ShieldBubble, transform);
                        }
                        else
                        {
                            m_ActivePowerup.transform.position = transform.position;
                        }

                        break;
                    }
                default:
                    {
                        SetBuffCooldownWidget(1.0f);

                        break;
                    }
            }

            bubbleBuffUseTimer -= Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        if (m_ActivePowerup)
            Destroy(m_ActivePowerup);

        m_CurrentBubbleBuff = BubbleBuff.BUFFTYPE.UNASSIGNED;
        SetBuffCooldownWidget(1.0f);
        IsUsingBubbleBuff = false;
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

    public void ApplyImpulse(Vector3 _impulse)
    {
        m_RigidBody.AddForce(_impulse * Time.fixedDeltaTime, ForceMode.Impulse);
    }
    public void ApplyForce(Vector3 _force)
    {
        m_RigidBody.AddForce(_force * Time.fixedDeltaTime, ForceMode.Acceleration);
    }

    public BubbleBuffUI GetUIWidget()
    {
        if (PlayerOne)
        {
            return m_BubbleBuffUIs[0];
        }
        else
        {
            return m_BubbleBuffUIs[1];
        }
    }
    void Drag()
    {
        ApplyForce(-m_RigidBody.velocity * DragForce);
    }
    
    void Movement()
    {
        ApplyForce(GetInput() * MoveSpeed);
    }
    
    void Boost()
    {
   
        if (CanMove)
        {
           if (!IsBoosting)
           {                
                StartCoroutine(BoostRoutine());
                             
                // Need to change this to a different thing as this is the same as getting hit
                //Destroy(Instantiate(Bubbles, BubblesPosition.position, BubblesRotation, gameObject.transform),5.0f);
            }
        }
    }

    IEnumerator BoostRoutine()
    { 
        oxygenTank.DashOxygenUse();
        float distance = Vector3.Distance(transform.position, Origin.position);
        float ikMaxDistance = Tether.CompleteLength;
        if (distance < ikMaxDistance && canDash)
        {
            // audio
            Destroy(Instantiate(audioDash), 2.0f);

            canDash = false;
            IsBoosting = true;
            Debug.Log("Boost! : " + (GetInput() * BoostForce).magnitude.ToString());
            m_RigidBody.velocity = GetInput() * BoostForce; //ApplyForce(GetInput() * BoostForce); // Saved incase bens boost sucks

            // Invulnerability for boost
            IsInvulnrable = true;
            m_InvulnrabilityTimer = DashingTime;

            yield return new WaitForSeconds(DashingTime);
            IsBoosting = false;
            m_RigidBody.velocity = new Vector3(); // Stops player
            ApplyForce(GetInput() * MoveSpeed * PostDashBoost); // tries to match pre-boost speed

            RemainingBoostTime = BoostCooldown;     
            while (RemainingBoostTime >= 0.0f)
            {
                RemainingBoostTime -= Time.deltaTime;
                SetBuffCooldownWidget(RemainingBoostTime / BoostCooldown);
                yield return new WaitForEndOfFrame();
            }

            RemainingBoostTime = BoostCooldown;
            SetBuffCooldownWidget(RemainingBoostTime / BoostCooldown);
            canDash = true;
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
        if (m_ActivePowerup != null)
        {
            BubbleShieldHit();
        }
        else if (!IsInvulnrable)
        {
            Debug.Log("Player Got Hit!");

            Destroy(Instantiate(Bubbles, BubblesPosition.position, BubblesRotation, gameObject.transform), 5.0f);
            Destroy(Instantiate(audioHurt), 2.0f);
            oxygenTank.DamageOxygenUse();
            StartInvulnrability();
            shake.StartShake(0.75f);
        }
    }


    public void BubbleShieldHit()
    {
        StartInvulnrability();
        IsUsingBubbleBuff = false;
    }
    
    public void PickupBubbleBuff(BubbleBuff.BUFFTYPE _bubbleBuff)
    {
        m_CurrentBubbleBuff = _bubbleBuff;
    }

    void SetAvailableBuffUI()
    {
        if (PlayerOne)
            BubbleBuffUI.P1UI.SetAvailableBuff(m_CurrentBubbleBuff);
        else
            BubbleBuffUI.P2UI.SetAvailableBuff(m_CurrentBubbleBuff);
    }

    void SetBuffCooldownWidget(float _fillAmount)
    {
        if (PlayerOne)
            BubbleBuffUI.P1UI.SetTimerImageFill(_fillAmount);
        else
            BubbleBuffUI.P2UI.SetTimerImageFill(_fillAmount);
    }

    public void StartInvulnrability()
    {
        IsInvulnrable = true;
        m_InvulnrabilityTimer = m_InvulnrabilityTime;
    }

    void RemoveInvulnrability()
    {
        IsInvulnrable = false;

        Debug.Log("Player Ready for more.");
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
            input.x--; 
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

    private void UpdateStretchyLine()
    {
        StretchyLine.SetPosition(0, gameObject.transform.position);
        if (PlayerOne)
        {
            StretchyLine.SetPosition(1, Player1Cord.GetComponentInChildren<FastIKFabric>().GetSpringPoint());
        }
        else
        {
            StretchyLine.SetPosition(1, Player2Cord.GetComponentInChildren<FastIKFabric>().GetSpringPoint());
        }
    }
}
