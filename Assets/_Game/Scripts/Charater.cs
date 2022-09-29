using System;
using UnityEngine;

public class Charater : MonoBehaviour
{
    [SerializeField]
    Animator playerAnim;
    [SerializeField] public HealthBar healthbar;
    [SerializeField] private CombatText combatText;
    private string curentAnim;


    private float damge;
    public float Damge { get => damge; set => damge = value; }

    [SerializeField]
    private float hp;
    public float Hp { get => hp; set => hp = value; }
    //public bool isDeath => hp <= 0;

    private bool isDeath;
    public bool IsDeath { get => hp <= 0; set => isDeath = value; }

    
    void Start()
    {
        OnInit();
    }

    // khoi tao mac dinh
    public virtual void OnInit()
    {
        //hp = 100;
        healthbar.Onit(100f);

    }

    // thay doi animation

    protected void ChangeAnim(string animName)
    {
        if (curentAnim != animName)
        {
            playerAnim.ResetTrigger(animName);

            curentAnim = animName;

            playerAnim.SetTrigger(curentAnim);
        }
    }

    // goi khi player chet
    protected virtual void OnDeath()
    {
        ChangeAnim("dead");
        Invoke(nameof(OnDespawn), 0.5f);
        
    }

    // ham huy

    public virtual void OnDespawn()
    {

    }
    // su kien bi tan cong
    public void OnHit(float damge)
    {
        if (!isDeath)
        {
            hp -= damge;
            healthbar.SetNewHp(hp);

            //  khoi tao combat text voi luong damge truyen vao
            Instantiate(combatText, transform.position + Vector3.up *2, Quaternion.identity).OnInit(damge);
            if (IsDeath)
            {
                hp = 0;
                OnDeath();
            }
        }
          
    }
    public virtual void Attack()
    {
        ChangeAnim("attack");
    }

    public virtual void HeavyAttack()
    {
        ChangeAnim("heavyattack");
    }

    public virtual void Throw()
    {
        ChangeAnim("throw");
    }

   
}
