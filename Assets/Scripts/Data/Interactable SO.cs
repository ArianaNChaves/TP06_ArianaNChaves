using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "Interactable Data", menuName = "ScriptableObjects/Interactable Data")]

public class InteractableSO : ScriptableObject
{
    [Header("Heal")]
    [SerializeField] private int healAmount;
    [SerializeField] private int maxHealthIncrease;
    
    [Header("Damage Boost")]
    [SerializeField] private int damageBoost;
    
    [FormerlySerializedAs("duration")]
    [Header("Invulnerability")]
    [SerializeField] private float invulnerabilityDuration;
    
    [FormerlySerializedAs("coinAmount")]
    [Header("Coin")]
    [SerializeField] private int coinValue;

    
    
    public int HealAmount{get => healAmount; set => healAmount = value; }
    public int MaxHealthIncrease{get => maxHealthIncrease; set => maxHealthIncrease = value; }
    public int DamageBoost{get => damageBoost; set => damageBoost = value; }
    public float Duration{get => invulnerabilityDuration; set => invulnerabilityDuration = value; }
    public int CoinAmount{get => coinValue; set => coinValue = value; }

}
