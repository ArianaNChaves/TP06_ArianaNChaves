using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Player Data", menuName = "ScriptableObjects/Player Data")]
public class EntitySO : ScriptableObject
{
    [Header("Movement Settings")]
    [SerializeField] private float movementSpeed;
    [SerializeField] private float jumpForce;


    [Header("Health Settings")]
    [SerializeField] private int maxHealth;

    [Header("Combat Settings")]
    [SerializeField] private int damage;
    
    public int Damage{get => damage; set => damage = value; }
    public float MovementSpeed { get => movementSpeed; set => movementSpeed = value; }
    public int MaxHealth { get => maxHealth; set => maxHealth = value; }
    public float JumpForce { get => jumpForce; set => jumpForce = value; }
}
