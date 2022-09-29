using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManagers : MonoBehaviour
{

    [SerializeField] Text coinText;
    [SerializeField] Text kunaiText;
    public static UIManagers instance;

    private void Awake()
    {
        if(instance==null)
        instance = this;
    }
    // Start is called before the first frame update
 

     public void setCoin(int coin)
    {
        coinText.text = coin.ToString();
    }

    public void SetKunaiAmount(int kunai)
    {
        kunaiText.text = kunai.ToString();
    }
}
