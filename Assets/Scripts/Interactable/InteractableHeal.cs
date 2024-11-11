using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableHeal : MonoBehaviour
{
    [SerializeField] private int healAmount;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        IHealthHandler healthHandler = other.GetComponent<IHealthHandler>();
        
        if (healthHandler == null) return;

        healthHandler.UpdateHealth(healAmount);
        Destroy(gameObject);
    }
}
