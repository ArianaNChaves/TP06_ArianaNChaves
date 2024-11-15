using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    public enum AnimationState
    {
        Walking,
        Attacking,
        Hitting,
        Jumping,
        Idle,
        Falling
    }
    
    [SerializeField] private Rigidbody2D rigidBody2D;
    [SerializeField] private EntitySO entityData;
    
    
    private Animator _animator;
    private AnimationState _currentAnimation;
    private bool _isGrounded = true;
    private float _timer = 0;
    private float _attackDuration = 0.350f;

    private static readonly int IsAttacking = Animator.StringToHash("isAttacking");

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }
    
    private void Update()
    {
        _timer += Time.deltaTime;
        AnimationHandler();
    }

    private void AnimationHandler()
    {
        AttackAnimation();
        if(_animator.GetBool(IsAttacking)) return;
        if (_isGrounded)
        {
            ChangeAnimation(Mathf.Abs(rigidBody2D.velocity.x) > 0f ? AnimationState.Walking : AnimationState.Idle);
        }

        _isGrounded = !(Mathf.Abs(rigidBody2D.velocity.y) > 0f);
        
        switch (rigidBody2D.velocity.y)
        {
            case < 0:
                ChangeAnimation(AnimationState.Falling);
                break;
            case > 0:
                ChangeAnimation(AnimationState.Jumping);
                break;
        }
    }

    private void AttackAnimation()
    {
        float attackDelay = _attackDuration > entityData.AttackRate ? _attackDuration : entityData.AttackRate;
            
        if (Input.GetKeyDown(KeyCode.Mouse0) && _timer >= attackDelay)
        {
            _timer = 0;
            ChangeAnimation(AnimationState.Attacking);
            _animator.SetBool(IsAttacking, true);
            Invoke(nameof(ChangeAttackStatus), _attackDuration);
        }
    }
    private void ChangeAttackStatus()
    {
        _animator.SetBool(IsAttacking, false);

    }
    

    private void ChangeAnimation(AnimationState newAnimation)
    {
        if (_currentAnimation == newAnimation) return;
        _animator.Play(EnumToString(newAnimation));
        _currentAnimation = newAnimation;
    }

    private string EnumToString(AnimationState animationState)
    {
        switch (animationState)
        {
            case AnimationState.Idle:
                return "Player Idle";
            case AnimationState.Walking:
                return "Player Move";
            case AnimationState.Hitting:
                return "Player Hit";
            case AnimationState.Jumping:
                return "Player Jump";
            case AnimationState.Falling:
                return "Player Fall";
            case AnimationState.Attacking:
                return "Player Attack";
            default:
                return "Unknown";
        }
    }
}
