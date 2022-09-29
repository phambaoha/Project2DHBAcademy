using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    // Start is called before the first frame update

    public Transform target;
    public Vector3 offSet;
    public float speed;

    private void Awake()
    {
        target = FindObjectOfType<PlayerController>().transform;
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, target.position + offSet, Time.deltaTime * speed);
    }
}
