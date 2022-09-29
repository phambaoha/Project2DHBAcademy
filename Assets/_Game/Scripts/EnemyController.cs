using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyController : Charater
{

    Rigidbody2D rb;
    public float attackRangeDamge;
    public float throwrangeDamge;
    public float moveSpeed;
    private IState currentState;

    bool isRight = true;

    private Charater target;
    public Charater Target { get => target; set => target = value; }

    public GameObject AttackArea;

    [SerializeField]
    GameObject[] itemDropPrefabs;

    // Start is called before the first frame update
    private void Awake()
    {
      
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
       
        // goi ham xu ly
        if (currentState != null)
        {
            currentState.OnExecute(this);
        }


    }

    // lua chon muc tieu
    public void SetTarget(Charater charater)
    {
        this.target = charater;
        // kiem tra target nam trong rangedamge
        if (IsTargetInRangeDamge())
        {
            ChangeState(new AttackState());
        }
        else
        if (target != null)
        {
            ChangeState(new PatrolState());

        }
        else
            ChangeState(new IdleState());
    }
    public override void OnInit()
    {
        base.OnInit();
        ChangeState(new IdleState());
        DeActiveAttack();
    }

   // ham huy
    public override void OnDespawn()
    {
        base.OnDespawn();
        Instantiate( itemDropPrefabs[UnityEngine.Random.Range(0,itemDropPrefabs.Length)],transform.position, Quaternion.identity);
        Destroy(gameObject);       
    }
    // su kien dead
    protected override void OnDeath()
    {
        base.OnDeath();
        ChangeState(null); 
    }

  

    // chuyen doi giua cac state
    public void ChangeState(IState newState)
    {
        if (currentState != null)
        {
            currentState.OnExit(this);
        }

        currentState = newState;

        if (currentState != null)
        {
            currentState.OnEnter(this);
        }
    }

    public void Moving()
    {
        ChangeAnim("run");

        rb.velocity = transform.right * moveSpeed;
    }

    // 
    public override void Attack()
    {
        base.Attack();

        ActiveAttack();

        Invoke(nameof(DeActiveAttack), 0.5f);
    }

    public override void Throw()
    {

    }
    public void StopMoving()
    {

        ChangeAnim("idle");
        rb.velocity = Vector2.zero;
    }


    // kiem tra doi tuong co nam trong tam danh
    public bool IsTargetInRangeDamge()
    {
        if (target != null && Vector2.Distance(target.transform.position, transform.position) <= attackRangeDamge)
        {
            return true;
        }
        else
            return false;
    }

    // kiem tra doi tuong trong tam throw
    public bool IsTargetInThrowRange()
    {
        if (target != null && Vector2.Distance(target.transform.position, transform.position) <= attackRangeDamge)
            return true;
        else
            return false;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "enemywall")
        {
            ChangeDir(isRight);
        }
    }


    // thay doi huong enemy 
    public void ChangeDir(bool isRight)
    {
        transform.rotation = isRight ? Quaternion.Euler(new Vector3(0, 180, 0)) : Quaternion.Euler(Vector3.zero);
        this.isRight = !isRight;
    }


    private void ActiveAttack()
    {
        AttackArea.SetActive(true);
    }

    private void DeActiveAttack()
    {
        AttackArea.SetActive(false);
    }
}
