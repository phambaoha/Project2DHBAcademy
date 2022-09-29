using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackArea : MonoBehaviour
{
    [SerializeField] float damge;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "enemy" || collision.gameObject.tag == "Player")
        {
            collision.GetComponent<Charater>().OnHit(damge);
        }
        
    }
}
