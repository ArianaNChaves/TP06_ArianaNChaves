using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableDamage : MonoBehaviour
{
    [SerializeField] private int damageAmount;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        IHealthHandler healthHandler = other.GetComponent<IHealthHandler>();
        
        if (healthHandler == null) return;

        Destroy(gameObject);
    }
}
