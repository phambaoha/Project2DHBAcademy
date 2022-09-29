using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class CombatText : MonoBehaviour
{

    [SerializeField] Text combatText;

    public void OnInit(float damge)
    {
        combatText.text = "- " + damge.ToString();
        Invoke(nameof(OnDespawn), 1f);
    }

    public void OnDespawn()
    {
        Destroy(gameObject);
    }
  
}
