using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CoinsManager : MonoBehaviour
{
    public static CoinsManager Instance;
    
    [SerializeField] private GameObject coinPrefab;
    
    private int _coinsAmount = 0;
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        GameplayUi.Instance.UpdateCoinsText(_coinsAmount);
    }


    public void AddCoins(int amount)
    {
        _coinsAmount += amount;
        GameplayUi.Instance.UpdateCoinsText(_coinsAmount);
    }

    public int GetCoinsAmount()
    {
        return _coinsAmount;
    }

    public void SpawnCoin(Vector3 position, Quaternion rotation)
    {
        Instantiate(coinPrefab, position, rotation);
    }
}
