using UnityEngine;

public class PlayerController : Charater
{
    // Start is called before the first frame update
    Rigidbody2D rb;
    float horizontal = 0;
    public float speed;
    public float jumpForce;
    [SerializeField]
    private LayerMask layerGround;

    private float holdTime = 0.3f;

    // private bool isJumping = false;

    private bool canDoubleJump;
    //public bool IsJumping { get => isJumping; set => isJumping = value; }

    private bool isAttack = false;




    Vector3 SavePoint;

    // kuani
    public GameObject kunaiPrefabs;
    private int  KunaiAmount = 10;
    public Transform ThrowPoint;

    // attack range
    public GameObject AttackArea;
    public GameObject heavyAttackArea;

    // dust player
    public GameObject DustPrefabs;
    public Transform PosSpawnDustPrefabs;


    private bool isClimbing = false;
  
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        SavePoint = transform.position;
    }

    float timer = 0;
    // Update is called once per frame
    void Update()
    {
     
        horizontal = Input.GetAxisRaw("Horizontal");
        if (Input.GetKey(KeyCode.G) && KunaiAmount >0)
        {

            Throw();
           
        }

        if (Input.GetKeyDown(KeyCode.W) && IsGrounded() && !canDoubleJump)
        {
            Jump();
            canDoubleJump = true;
        }
        else if (Input.GetKeyDown(KeyCode.W) && canDoubleJump)
        {
            canDoubleJump = false;
            Jump();

        }


        if (IsGrounded())
        {
            if (Input.GetKeyDown(KeyCode.C))
            {
                timer = Time.time;
            }
            if (Input.GetKey(KeyCode.C))
            {
                if (timer + holdTime <= Time.time)
                {
                    HeavyAttack();
                }

            }

            if (Input.GetKeyDown(KeyCode.F))
            {
                Attack();
            }
            if (Mathf.Abs(horizontal) > 0f)
                ChangeAnim("run");
        }
        else
         if (!IsGrounded() && rb.velocity.y < 0)
        {
            ChangeAnim("fall");
            //  isJumping = false;
        }


        // khi da attack thi khong chay move nua
        if (isAttack)
        {
            rb.velocity = Vector2.zero;
            return;
        }

        // di chuyen
        Move();
    }

    // khoi tao cua player
    public override void OnInit()
    {
        base.OnInit();
        transform.position = SavePoint;
        canDoubleJump = false;
        canSpawnDustWhileColGround = true;
        UIManagers.instance.setCoin(0);
        UIManagers.instance.SetKunaiAmount(KunaiAmount);
    }

    protected override void OnDeath()
    {
        base.OnDeath();
    }
    public override void OnDespawn()
    {
        base.OnDespawn();

        Invoke(nameof(OnInit), 0.5f);

    }
    // di chuyen trai phai
    void Move()
    {


        if (Mathf.Abs(horizontal) > 0f)
        {
            rb.velocity = new Vector2(horizontal * speed * Time.fixedDeltaTime, rb.velocity.y);
            transform.rotation = Quaternion.Euler(new Vector3(0, horizontal > 0 ? 0 : 180, 0));
        }
        // check CheckGround()
        else if (IsGrounded())
        {
            ChangeAnim("idle");
            rb.velocity = Vector2.zero;
        }
    }



    public void Jump()
    {

        ChangeAnim("jump");
        rb.AddForce(jumpForce * Vector2.up);
        //isJumping = true;
        canSpawnDustWhileColGround = true;
        Instantiate(DustPrefabs, transform.position + Vector3.down, Quaternion.identity);
    }

    public override void Attack()
    {

        base.Attack();
        Invoke(nameof(ResetAttack), 0.5f);
        isAttack = true;
        ActiveAttack();
        Invoke(nameof(DeActiveAttack), 0.1f);
    }

    public override void HeavyAttack()
    {
        base.HeavyAttack();
        Invoke(nameof(ResetAttack), 0.6f);
        isAttack = true;
        HeavyAttActive();
        Invoke(nameof(HeavyAttDeActive), 0.5f);
    }

    public override void Throw()
    {
        if (isAttack)
            return;
        else
        {
            KunaiAmount--;
            UIManagers.instance.SetKunaiAmount(KunaiAmount);
            base.Throw();
            Invoke(nameof(ResetAttack), 0.5f);
            isAttack = true;
            Instantiate(kunaiPrefabs, ThrowPoint.position, ThrowPoint.rotation);
        }

    }
    void ResetAttack()
    {
        ChangeAnim("ilde");
        isAttack = false;

    }


    bool IsGrounded()
    {
         Debug.DrawLine(transform.position, transform.position + Vector3.down, Color.red);
        RaycastHit2D hit2D = Physics2D.Raycast(transform.position, Vector3.down, 1.2f, layerGround);

        //if (hit2D.collider != null)
        //{
        //    return true;
        //}
        //else
        //return false;

        return hit2D.collider != null;
    }



    int coin = 0;

    bool isColisionWithSavePoint = false;



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "coin")
        {
            coin++;
            UIManagers.instance.setCoin(coin);
            Destroy(collision.gameObject);

        }
        if (collision.gameObject.tag == "boxdead")
        {
            ChangeAnim("dead");
            Invoke(nameof(OnInit), 1f);

        }
        if (collision.gameObject.tag == "savepoint" && !isColisionWithSavePoint)
        {
            isColisionWithSavePoint = true;
            SavePoint = collision.transform.position;
        }
        if (collision.gameObject.tag == "health")
        {
            Hp += 10;
            healthbar.SetNewHp(Hp);
            Destroy(collision.gameObject);

        }
        if (collision.gameObject.tag == "kunai")
        {
            KunaiAmount++;
            UIManagers.instance.SetKunaiAmount(KunaiAmount);
            Destroy(collision.gameObject);

        }

        if(collision.gameObject.tag == "rope")
        {
            print("vao rope");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "rope")
        {
            print(" thoat rope");
        }
    }

    private bool canSpawnDustWhileColGround;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "ground" && canSpawnDustWhileColGround)
        {
            Instantiate(DustPrefabs, transform.position + Vector3.down, Quaternion.identity);
            canSpawnDustWhileColGround = false;
        }
    }

    private void ActiveAttack()
    {
        AttackArea.SetActive(true);
    }

    private void DeActiveAttack()
    {
        AttackArea.SetActive(false);
    }
    private void HeavyAttActive()
    {
        heavyAttackArea.SetActive(true);
    }

    private void HeavyAttDeActive()
    {
        heavyAttackArea.SetActive(false);
    }


    public void SetMove(float hoz)
    {
        horizontal = hoz;
    }

    public void SetStopMoving()
    {
        horizontal = 0;
    }





}
