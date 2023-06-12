using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldScript : MonoBehaviour
{
    public bool isShielded;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ActivateShield()
    {
        gameObject.SetActive(true);
        isShielded = true;
    }

    public void GoCooldown()
    {
        gameObject.SetActive(false);
        isShielded = false;
        Invoke(nameof(ActivateShield), 5f);
    }
}
