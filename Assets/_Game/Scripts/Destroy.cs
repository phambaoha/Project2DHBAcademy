using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        OnInit();
    }

    void OnInit()
    {
        Invoke(nameof(OnDespawn), 0.5f);
    } 
    
    void OnDespawn()
    {
        Destroy(gameObject);
    }    

}
