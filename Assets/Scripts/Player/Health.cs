using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class Health : MonoBehaviour, IHealthHandler
{
    [SerializeField] private Image healthBar;
    // [SerializeField] private GameObject particles;
    [SerializeField] private EntitySO entityData;
    
    private int _health;
    private void Start()
    {
        _health = entityData.MaxHealth;
        UpdateHealthBar();
    }
    
    public void UpdateHealth(int amount)
    {
        _health += amount;
        if (_health <= 0)
        {
            Die();
        }
        if (_health > entityData.MaxHealth)
        {
            _health = entityData.MaxHealth;
        }

        UpdateHealthBar();
    }
    private void UpdateHealthBar()
    {
        float clampedHealth = Mathf.Clamp(_health, 0, entityData.MaxHealth);
        healthBar.fillAmount = clampedHealth / entityData.MaxHealth;
    }
    private void Die()
    {
        // Instantiate(particles, transform.position, Quaternion.identity);
        Destroy(gameObject,0.01f);
    }
}
