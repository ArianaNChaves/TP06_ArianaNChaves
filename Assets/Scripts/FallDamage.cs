using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FallDamage : MonoBehaviour
{
    private const int KillDamage = -99999;
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        IHealthHandler healthHandler = other.gameObject.GetComponent<IHealthHandler>();
        healthHandler.UpdateHealth(KillDamage);
    }

    // private void OnTriggerEnter2D(Collider2D other)
    // {
    //     IHealthHandler healthHandler = other.gameObject.GetComponent<IHealthHandler>();
    //     healthHandler.UpdateHealth(KillDamage);
    // }
}
