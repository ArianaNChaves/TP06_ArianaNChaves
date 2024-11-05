using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Player Data", menuName = "ScriptableObjects/Player Data")]
public class PlayerSO : ScriptableObject
{
    [Header("Player Movement Settings")]
    [SerializeField] private float movementSpeed;

    [SerializeField] private float jumpForce;


    [Header("Player Health Settings")]
    [SerializeField] private int maxHealth;

    [Header("Player Bullet Settings")]
    [SerializeField] private int damage;
    [SerializeField] private float fireRate;
    [SerializeField] private float velocity;
    [SerializeField] private float lifeTime;
    
    public int Damage{get => damage; set => damage = value; }
    public float Velocity { get => velocity; set => velocity = value; }
    public float LifeTime { get => lifeTime; set => lifeTime = value; }
    public float MovementSpeed { get => movementSpeed; set => movementSpeed = value; }
    public int MaxHealth { get => maxHealth; set => maxHealth = value; }
    public float JumpForce { get => jumpForce; set => jumpForce = value; }
    public float FireRate { get => fireRate; set => fireRate = value; }
}
