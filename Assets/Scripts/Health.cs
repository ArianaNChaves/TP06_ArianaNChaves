using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class Health : MonoBehaviour, IHealthHandler
{
    private static readonly int IsDead = Animator.StringToHash("isDead");
    [SerializeField] private Image healthBar;
    [SerializeField] private Animator animator;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private EntitySO entityData;
    [SerializeField] private Color low;
    [SerializeField] private Color high;
    
    private int _health;
    private int _maxHealth;
    private bool _canReceiveDamage = true;
    private bool _isThePlayer;
    private Color _defaultColor = Color.white;

    private void Start()
    {
        _maxHealth = entityData.MaxHealth;
        _health = _maxHealth;
        UpdateHealthBar();
        _isThePlayer = this.gameObject.CompareTag("Player");
    }
    
    public void UpdateHealth(int amount)
    {
        int value = amount;
        if (amount < 0 && !_canReceiveDamage)
        {
            value = 0;
        }

        if (value < 0 && _isThePlayer)
        {
            StartCoroutine(HittingAnimation());
            AudioManager.Instance.PlayEffect("Hit");
        }
            
        _health += value;
        
        if (_health <= 0)
        {
            Die();
        }
        if (_health > _maxHealth)
        {
            _health = _maxHealth;
        }

        UpdateHealthBar();
    }
    
    private void UpdateHealthBar()
    {
        float clampedHealth = Mathf.Clamp(_health, 0, _maxHealth);
        healthBar.fillAmount = clampedHealth / _maxHealth;
    }
    
    private void Die()
    {
        bool wasCoinDroped = false;
        if (!_isThePlayer && !wasCoinDroped)
        {
            wasCoinDroped = true;
            animator.SetBool(IsDead, true);
            animator.Play("Enemy Death");
            CoinsManager.Instance.SpawnCoin(this.gameObject.transform.position, this.gameObject.transform.rotation);
            AudioManager.Instance.PlayEffect("Enemy Die");
        }
        if (_isThePlayer)
        {
            AudioManager.Instance.PlayEffect("Lose");
            Lose();
        }
        Destroy(gameObject,0.35f);
    }

    private IEnumerator InvulnerabilityColoring()
    {
        while (!_canReceiveDamage)
        {
            spriteRenderer.color = low;
            yield return new WaitForSeconds(0.08f);

            spriteRenderer.color = high;
            yield return new WaitForSeconds(0.08f);
        }

        spriteRenderer.color = Color.white;
        
    }

    private IEnumerator Invulnerability(float invulnerabilityTime)
    {
        _canReceiveDamage = false;
        
        yield return new WaitForSeconds(invulnerabilityTime);  
        
        _canReceiveDamage = true;
        
    }

    private IEnumerator HittingAnimation()
    {
        animator.SetBool("wasHitting", true);

        yield return new WaitForSeconds(0.15f);  
        
        animator.SetBool("wasHitting", false);
        
    }
    
    public void ActivateInvulnerability(float invulnerabilityTime)
    {
        StartCoroutine(Invulnerability(invulnerabilityTime));
        StartCoroutine(InvulnerabilityColoring());
    }

    public void IncreaseMaxHealth(int amount)
    {
        _maxHealth += amount;
        UpdateHealthBar();
    }

    private void Lose()
    {
        GameData.Instance.AddCoins(CoinsManager.Instance.GetMaxCoinsAmount());
        SceneManager.LoadScene("LoseScreen");
    }
}
