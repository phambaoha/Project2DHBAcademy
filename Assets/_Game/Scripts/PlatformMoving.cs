using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlatformMoving : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    Vector3 startPos, EndPos;

    Vector3 CurentPos;

    public float speed = 5f;
    void Start()
    {
       // transform.position = startPos;
        CurentPos = EndPos;
    }

    private void FixedUpdate()
    {
        transform.position = Vector3.MoveTowards(transform.position, CurentPos, speed *  Time.deltaTime);

        if( Vector3.Distance(transform.position, EndPos)< 0.1f)
        {
            CurentPos = startPos;
        }
        if(Vector3.Distance(transform.position,startPos) < 0.1f)
        {
            CurentPos = EndPos;
        }
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.transform.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 20);
            collision.transform.SetParent(transform);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.transform.SetParent(null );
        }
    }
  
}
