using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using Photon.Voice.Unity;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

[RequireComponent(typeof(PhotonView))]
[RequireComponent(typeof(SpriteRenderer))]
public class CharacterMovement : MonoBehaviourPun, IPunObservable
{
    public float moveSpeed;
    public Rigidbody2D rb2d;
    private Vector2 moveInput;
    public float activeMoveSpeed;
    Animator animator;
    public TrailRenderer tr;
    private SpriteRenderer spriteRenderer;
    private bool canDash = true;
    private bool isDashing;
    private float dashingPower = 24f;
    private float dashingTime = 0.2f;
    private float dashingCooldown = 5f;
    private bool canHit = true;
    private bool isHitting;
    private float hittingTime = 0.2f;
    private float HittingCooldown = 3f;
    private bool canRiposte = true;
    private bool isRiposte;
    private float riposteTime = 2f;
    private float riposteCooldown = 3f;
    private PhotonView m_view;
    private GameObject healthBar;
    private Health health;
    private GameObject gameoversc;
    public GameObject mngonline;
    [SerializeField] private TMP_Text nameText;
    private bool isSpeedUp;
    float SpawnTimer = 5f;
    private float SpeedUpVal = 6;
    public string username = "ads";
    float cntdwn = 5.0f;
    public float characterHealth;
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;
    private GameObject waitScreen;
    private GameObject Indicator;
    private Recorder photonVoiceRecorder;

    [SerializeField] private Sprite mic_off;

    [SerializeField] private Sprite mic_on;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.tag = "Player";
        tr = GetComponent<TrailRenderer>();
        activeMoveSpeed = moveSpeed;
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        m_view = GetComponent<PhotonView>();
        AddObservable();
        gameObject.AddComponent<Health>();
        health = gameObject.GetComponent<Health>();
        gameoversc = GameObject.Find("Canvas");
        mngonline = GameObject.Find("Manager");
        nameText.text = m_view.Owner.NickName;

        foreach (Transform eachChild in gameoversc.transform)
        {
            if (eachChild.name == "WaitingScreen")
            {
                waitScreen = eachChild.gameObject;
            }

            if (eachChild.name == "VoiceIndicator")
            {
                Indicator = eachChild.gameObject;
            }
        }

        photonVoiceRecorder = GameObject.Find("VoiceManager").GetComponent<Recorder>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!m_view.IsMine) return;
        waitingScreen();
        if (m_view.IsMine && (waitScreen.activeSelf == false || getHeal() == 0))
        {
            healthBarSync();
            characterHealth = getHeal();


            if (isDashing)
            {
                return;
            }

            moveInput.x = Input.GetAxisRaw("Horizontal");
            moveInput.y = Input.GetAxisRaw("Vertical");
            moveInput.Normalize();
            speedConfig();
            Vector3 vel = transform.rotation * rb2d.velocity;
            if (rb2d.velocity.magnitude > 0)
                animator.SetBool("isMoving", true);
            else
                animator.SetBool("isMoving", false);

            if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)
            {
                StartCoroutine(Dash());
            }

            if (Input.GetKeyDown(KeyCode.Q))
            {
                photonVoiceRecorder.TransmitEnabled = true;
                Indicator.transform.Find("IndicatorImage").GetComponent<Image>().sprite = mic_on;
            }
            
            if (Input.GetKeyUp(KeyCode.Q))
            {
                photonVoiceRecorder.TransmitEnabled = false;
                Indicator.transform.Find("IndicatorImage").GetComponent<Image>().sprite = mic_off;
            }
            if (Input.GetKeyDown(KeyCode.Space)&& canRiposte)
            {
                m_view.RPC("Riposte",RpcTarget.AllBuffered,false);
            }
           
            if (characterHealth == 0)
            {
                animator.SetTrigger("Death");
                cntdwn -= Time.deltaTime;
                if (cntdwn < 0)
                {
                    deathScreen();
                }
            }

            m_view.RPC("checkState", RpcTarget.AllBuffered, false);
            if (Input.GetKeyDown(KeyCode.Mouse0) && canHit)
            {
                StartCoroutine(Attack());
            }

            else if (vel.x > 0)
            {
                spriteRenderer.flipX = true;
                transform.GetChild(4).gameObject.transform.localPosition = new Vector3(0.618f,
                    transform.GetChild(4).gameObject.transform.localPosition.y,
                    transform.GetChild(4).gameObject.transform.localPosition.z);
            }
            else if (vel.x < 0)
            {
                spriteRenderer.flipX = false;
                transform.GetChild(4).gameObject.transform.localPosition = new Vector3(-0.618f,
                    transform.GetChild(4).gameObject.transform.localPosition.y,
                    transform.GetChild(4).gameObject.transform.localPosition.z);
            }
        }
    }

    private void FixedUpdate()
    {
        if (!m_view.IsMine) return;
    }


    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        float originalGravity = rb2d.gravityScale;
        rb2d.gravityScale = 0f;
        rb2d.velocity = new Vector2(transform.localScale.x * dashingPower * Input.GetAxisRaw("Horizontal"),
            transform.localScale.y * dashingPower * Input.GetAxisRaw("Vertical"));
        tr.emitting = true;
        yield return new WaitForSeconds(dashingTime);
        tr.emitting = false;
        rb2d.gravityScale = originalGravity;
        isDashing = false;
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }

    public void resetDashCooldown()
    {
        canDash = true;
    }

    public float getHeal()
    {
        return health.getHeal();
    }

    public void speedConfig()
    {
        if (isSpeedUp)
        {
            rb2d.velocity = moveInput * SpeedUpVal;
            SpawnTimer -= Time.deltaTime;
            if (SpawnTimer <= 0f)
            {
                isSpeedUp = false;
                SpawnTimer = 5f;
            }
        }
        else
        {
            rb2d.velocity = moveInput * activeMoveSpeed;
        }
    }

    public void IncreaseSpeed()
    {
        isSpeedUp = true;
    }


    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(spriteRenderer.flipX);
        }
        else
        {
            spriteRenderer.flipX = (bool)stream.ReceiveNext();
        }
    }


    private void AddObservable()
    {
        if (!m_view.ObservedComponents.Contains(this))
        {
            m_view.ObservedComponents.Add(this);
        }
    }

    public void destroyCharacter()
    {
        Destroy(gameObject);
    }

    public void waitingScreen()
    {
        if (mngonline.GetComponent<ManageOnline>().playerCount == 2)
        {
            foreach (Transform eachChild in gameoversc.transform)
            {
                if (eachChild.name == "WaitingScreen")
                {
                    eachChild.gameObject.SetActive(false);
                }
            }
        }
    }

    public void deathScreen()
    {
        destroyCharacter();
        mngonline.GetComponent<ManageOnline>().DisconnectPlayer();
        foreach (Transform eachChild in gameoversc.transform)
        {
            if (eachChild.name == "GameOverScreen")
            {
                eachChild.gameObject.SetActive(true);
            }
        }
    }

    public IEnumerator Attack()
    {
        canHit = false;
        isHitting = true;
        animator.SetTrigger("Attack");
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
        foreach (Collider2D enemy in hitEnemies)
        {
            Debug.Log("We hit" + enemy.name);

            Debug.Log(enemy.gameObject.GetComponent<PhotonView>().Owner.NickName);
        }

        yield return new WaitForSeconds(hittingTime);
        isHitting = false;
        yield return new WaitForSeconds(HittingCooldown);
        canHit = true;
    }
    [PunRPC]
    public IEnumerator Riposte(bool b)
    {
        canRiposte = false;
        isRiposte = true;
        animator.SetTrigger("Riposte");
        yield return new WaitForSeconds(riposteTime);
        isRiposte = false;
        animator.SetTrigger("ReturnIdle");
        yield return new WaitForSeconds(riposteCooldown);
        canRiposte = true;
    }


    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.collider.name);
        if (collision.collider.name == "AttackPoint" && collision.gameObject.name == "AttackPoint")
        {
            if (!isRiposte)
            {
                Debug.Log("it hitted");
                health.damage(10);
                animator.SetTrigger("Hurt");
            }
            
        }
    }


    public void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
        {
            return;
        }

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

    [PunRPC]
    public void checkState(bool b)
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
        {
            transform.GetChild(4).gameObject.SetActive(true);
        }
        else
        {
            transform.GetChild(4).gameObject.SetActive(false);
        }
    }
    
   

    private void healthBarSync()
    {
        foreach (Transform eachChild in transform)
        {
            if (eachChild.name == "pfHealthBar")
            {
                eachChild.localScale = new Vector3((health.getHeal() / 100) * 1, 1, 1);
            }
        }
    }
}