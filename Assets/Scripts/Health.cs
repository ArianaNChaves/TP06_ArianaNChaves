using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class Health : MonoBehaviour, IHealthHandler
{
    private static readonly int IsDead = Animator.StringToHash("isDead");
    [SerializeField] private Image healthBar;
    [SerializeField] private Animator animator;
    [SerializeField] private EntitySO entityData;
    
    private int _health;
    private bool _canReceiveDamage = true;
    private bool _isThePlayer;
    private PlayerAnimations _playerAnimations;

    private void Start()
    {
        _playerAnimations = GetComponentInChildren<PlayerAnimations>();
        _health = entityData.MaxHealth;
        UpdateHealthBar();
        GameplayUi.Instance.UpdateMaxHealthText(entityData.MaxHealth);
        _isThePlayer = this.gameObject.CompareTag("Player");
    }
    
    public void UpdateHealth(int amount)
    {
        int value = amount;
        if (amount < 0 && !_canReceiveDamage)
        {
            value = 0;
            Debug.Log("NO PUEDE RECIBIR DANIO PERRA ~ Health.cs/UpdateHealth");
        }
            
        _health += value;
        
        if (_health <= 0)
        {
            Die();
        }
        if (_health > entityData.MaxHealth)
        {
            _health = entityData.MaxHealth;
        }

        UpdateHealthBar();
        if (_isThePlayer && value < 0)
        {
            _playerAnimations.GettingHit();
        }
    }
    
    private void UpdateHealthBar()
    {
        float clampedHealth = Mathf.Clamp(_health, 0, entityData.MaxHealth);
        healthBar.fillAmount = clampedHealth / entityData.MaxHealth;
    }
    
    private void Die()
    {
        // Instantiate(particles, transform.position, Quaternion.identity);
        bool wasCoinDroped = false;
        if (!_isThePlayer && !wasCoinDroped)
        {
            wasCoinDroped = true;
            animator.SetBool(IsDead, true);
            animator.Play("Enemy Death");
            CoinsManager.Instance.SpawnCoin(this.gameObject.transform.position, this.gameObject.transform.rotation);
        }
        Destroy(gameObject,0.35f);
    }

    private IEnumerator Invulnerability(float invulnerabilityTime)
    {
        _canReceiveDamage = false;
        
        yield return new WaitForSeconds(invulnerabilityTime);  
        
        _canReceiveDamage = true;
        
    }
    
    public void ActivateInvulnerability(float invulnerabilityTime)
    {
        StartCoroutine(Invulnerability(invulnerabilityTime));
    }

    public void IncreaseMaxHealth(int amount)
    {
        entityData.MaxHealth += amount;
        UpdateHealthBar();
        GameplayUi.Instance.UpdateMaxHealthText(entityData.MaxHealth);
    }
}
