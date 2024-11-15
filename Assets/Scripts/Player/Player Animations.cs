using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rigidBody2D;

    
    private Animator _animator;

    private static readonly int MoveSpeed = Animator.StringToHash("Move Speed");
    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void Move(float horizontalMovement)
    {
        _animator.SetFloat(MoveSpeed, Mathf.Abs(horizontalMovement));
    }
}
