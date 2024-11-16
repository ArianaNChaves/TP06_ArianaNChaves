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
    [SerializeField] private GameObject healPrefab;
    
    [Header("Interact")]
    [SerializeField] private GameObject interactObj;
    [SerializeField] private GameObject notEnoughCoins;

    [Header("Settings")]
    [SerializeField] private GameSettingsSO gameSettingsData;
    [SerializeField] private Transform throwPosition;
    [SerializeField] private int candyCost;
    
    private PlayerMovement _playerMovement;
    private Animator _animator;
    private bool _hasEnoughCoins;
    private bool _isShopping = false;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        _hasEnoughCoins = CoinsManager.Instance.GetCoinsAmount() >= candyCost;
        ShowText();
        if (Input.GetKeyDown(KeyCode.E))
        {
            CheckCoins();
        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        _isShopping = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        _isShopping = false;
        interactObj.SetActive(false);
        notEnoughCoins.SetActive(false);
        
    }

    private void ShowText()
    {
        if (!_isShopping) return;
        if (_hasEnoughCoins)
        {
            _animator.SetBool("isBuying",true);
            interactObj.SetActive(true);
            notEnoughCoins.SetActive(false);
        }
        else
        {
            _animator.SetBool("isBuying",false);
            notEnoughCoins.SetActive(true);
            interactObj.SetActive(false);
        }
    }

    private void CheckCoins()
    {
        if (!_hasEnoughCoins) return;
        ThrowCandy();
    }

    private void ThrowCandy()
    {   
        
        int randomCandy = Random.Range(0, 2);
        
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
            default:
            {
                Debug.Log("Throw Candy ~ random candy switch");
                break;
            }
        }
    }
}
