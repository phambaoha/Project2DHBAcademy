using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{

    [SerializeField] Image imageFill;
    float hp;
    float maxHp;

    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void Onit(float maxHp)
    {
        hp = maxHp;
        this.maxHp = maxHp;
        imageFill.fillAmount = 1;
    }


    // gan lai gia tri cho hp
    public void SetNewHp(float hp)
    {
        this.hp = hp;
    }
      
    // Update is called once per frame
    void Update()
    {
        imageFill.fillAmount =  Mathf.Lerp(imageFill.fillAmount, hp / maxHp, Time.deltaTime * 5f);
    }
}
