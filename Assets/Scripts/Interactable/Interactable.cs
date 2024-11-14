using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class Interactable : MonoBehaviour
{
    private enum Type
    {
        Heal,
        IncreaseMaxHealth,
        DamageBoost,
        Invulnerability,
        AddJumps,
        Coin,
    }
    
    [SerializeField] private Type type;
    [SerializeField] private InteractableSO interactableData;
    
    private const int MinCoinsAmount = 2;
    private const int MaxCoinsAmount = 5;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) return;
        
        EnumHandler(collision.gameObject);
    }

    private void EnumHandler(GameObject obj)
    {
        switch (type)
        {
            case Type.Heal:
            {
                HealPlayer(obj);
                break;
            }
            case Type.DamageBoost:
            {
                BoostPlayerDamage(obj);
                break;
            }
            case Type.Invulnerability:
            {
                ActivatePlayerInvulnerability(obj);
                break;
            }
            case Type.AddJumps:
            {
                AddPlayerJumps(obj);
                break;
            }
            case Type.IncreaseMaxHealth:
            {
                IncreasePlayerMaxHealth(obj);
                break;
            }case Type.Coin:
            {
                AddCoins();
                break;
            }
            default:
            {
                Debug.LogError("Wrong Interactable Type ~ Interactable.cs/EnumHandler");
                break;
            }
        }
    }

    private void HealPlayer(GameObject obj)
    {
        IHealthHandler healthHandler = obj.GetComponent<IHealthHandler>();
        
        if (healthHandler == null) return;

        healthHandler.UpdateHealth(interactableData.HealAmount);
        Destroy(this.gameObject);
    }

    private void BoostPlayerDamage(GameObject obj)
    {
        PlayerAttack playerAttack = obj.GetComponent<PlayerAttack>();
        
        if (playerAttack == null) return;
        
        playerAttack.IncreaseDamage += interactableData.DamageBoost;
        GameplayUi.Instance.UpdateDamageText(playerAttack.IncreaseDamage);
        Destroy(this.gameObject);
    }

    private void ActivatePlayerInvulnerability(GameObject obj)
    {
        Health playerHealth = obj.GetComponent<Health>();
        
        if(playerHealth == null) return;
        
        playerHealth.ActivateInvulnerability(interactableData.Duration);
        Destroy(this.gameObject);
    }

    private void AddPlayerJumps(GameObject obj)
    {
        PlayerMovement playerMovement = obj.GetComponent<PlayerMovement>();
        
        if(playerMovement == null) return;

        playerMovement.AddMaxJumps();
        Destroy(this.gameObject);
    }

    private void IncreasePlayerMaxHealth(GameObject obj)
    {
        Health playerHealth = obj.GetComponent<Health>();
        
        if(playerHealth == null) return;
        playerHealth.IncreaseMaxHealth(interactableData.MaxHealthIncrease);
        
        Destroy(this.gameObject);
    }

    private void AddCoins()
    {
        Debug.Log("MAS GUITA PA");
        int currentCoinsAmount = Random.Range(MinCoinsAmount, MaxCoinsAmount + 1);
        CoinsManager.Instance.AddCoins(currentCoinsAmount);
        Destroy(this.gameObject);

    }
}
