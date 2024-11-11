using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Entity Data", menuName = "ScriptableObjects/Entity Data")]
public class EntitySO : ScriptableObject
{
    [Header("Movement Settings")]
    [SerializeField] private float movementSpeed;
    [SerializeField] private float jumpForce;
    [SerializeField] private int startJumpsAmount;

    [Header("Health Settings")]
    [SerializeField] private int maxHealth;

    [Header("Combat Settings")]
    [SerializeField] private int damage;
    [SerializeField] private float attackRate;
    
    public int Damage{get => damage; set => damage = value; }
    public float MovementSpeed { get => movementSpeed; set => movementSpeed = value; }
    public float AttackRate { get => attackRate; set => attackRate = value; }
    public int MaxHealth { get => maxHealth; set => maxHealth = value; }
    public int StartJumpsAmount { get => startJumpsAmount; set => startJumpsAmount = value; }
    public float JumpForce { get => jumpForce; set => jumpForce = value; }
}
