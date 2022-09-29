using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kunai : MonoBehaviour
{
    [SerializeField] private float kunaiDamge;
    public GameObject BloodDrop;
    public Rigidbody2D rb;
    public float speed;

    
    void Start()
    {
        OnInit();
       
    }

    public void OnInit()
    {
        rb.velocity = transform.right  * speed;

        Invoke(nameof(OnDeSpawn), 1.5f);
    }
    public void OnDeSpawn()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "enemy")
         {
            Instantiate(BloodDrop, transform.position,transform.rotation);
            collision.GetComponent<Charater>().OnHit(kunaiDamge);
            OnDeSpawn();
        }
    }

    
}

