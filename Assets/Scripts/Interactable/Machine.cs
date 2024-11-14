using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class Machine : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField] private GameObject maxHealthPrefab;
    [SerializeField] private GameObject damageBoostPrefab;
    [SerializeField] private GameObject extraJumpsPrefab;
    [SerializeField] private GameObject healPrefab;
    
    [Header("Interact")]
    [SerializeField] private GameObject interactObj;
    [SerializeField] private GameObject notEnoughCoins;

    [Header("Settings")]
    [SerializeField] private GameSettingsSO gameSettingsData;
    [SerializeField] private Transform throwPosition;
    [SerializeField] private int candyCost;
    
    private PlayerMovement _playerMovement;
    private bool _hasEnoughCoins;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            CheckCoins();
        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        
        _hasEnoughCoins = CoinsManager.Instance.GetCoinsAmount() >= candyCost;
        _playerMovement = other.gameObject.GetComponent<PlayerMovement>();
        
        if (_hasEnoughCoins)
        {
            interactObj.SetActive(true);
        }
        else
        {
            notEnoughCoins.SetActive(true);
        }
        
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        interactObj.SetActive(false);
        notEnoughCoins.SetActive(false);
        
    }

    private void CheckCoins()
    {
        _hasEnoughCoins = CoinsManager.Instance.GetCoinsAmount() >= candyCost;
        if (!_hasEnoughCoins) return;
        ThrowCandy();
    }

    private void ThrowCandy()
    {
        int randomCandy = Random.Range(0, 3);
        
        CoinsManager.Instance.ExpendCoins(candyCost);
        switch (randomCandy)
        {
            case 0:
            {
                Instantiate(maxHealthPrefab, throwPosition.position, Quaternion.identity);
                break;
            }
            case 1:
            {
                Instantiate(damageBoostPrefab, throwPosition.position, Quaternion.identity);
                break;
            }
            case 2:
            {
                Instantiate(_playerMovement.GetMaxJumps() <= gameSettingsData.MaxJumps ? extraJumpsPrefab : healPrefab,
                    throwPosition.position, Quaternion.identity);
                break;
            }
            default:
            {
                Debug.Log("Throw Candy ~ random candy switch");
                break;
            }
        }
    }
}
