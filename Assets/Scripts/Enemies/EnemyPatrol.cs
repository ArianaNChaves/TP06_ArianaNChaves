using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private EntitySO entityData;
    
    [Header("Patrol")]
    [SerializeField] private Transform positionA;
    [SerializeField] private Transform positionB;
    [SerializeField] private Transform body;
    [SerializeField] private float maxDistance;
    [SerializeField] private float patrolWaitTime;
    [SerializeField] private bool isFacingRight;

    private State _state;
    private Rigidbody2D _rigidbody2D;
    private bool _isMovingToB;
    private bool _isWaiting = false;
    private bool _isChangingState = false;
    private enum State
    {
        Patrol,
        Attack,
        Death,
    }

    private void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _state = State.Patrol;
    }

    private void Update()
    {
        CheckState();

    }


    private void CheckState()
    {
        if (_isChangingState) return;
        switch (_state)
        {
            case State.Patrol:
            {
                Patrol();
                break;
            }
        }
    }

    private void Patrol()
    {
        if (_isWaiting) return;
        Vector2 target = new Vector2(_isMovingToB ? positionB.position.x : positionA.position.x, transform.position.y);
        Vector2 direction = (target - (Vector2)transform.position).normalized;
        _rigidbody2D.velocity = new Vector2(direction.x * entityData.MovementSpeed, transform.position.y);
        
        if (Mathf.Abs(transform.position.x - target.x) < maxDistance && !_isWaiting)
        {
            StartCoroutine(ChangeDirection());
        }
    }

    private IEnumerator ChangeDirection()
    {
        _rigidbody2D.velocity = Vector2.zero;
        _isWaiting = true;
        yield return new WaitForSeconds(patrolWaitTime); 
        Utilities.RotateObject(ref isFacingRight, ref body);
        _isMovingToB = !_isMovingToB;
        _isWaiting = false;
    }

    private void ChangingState()
    {
        _isChangingState = true;
        _rigidbody2D.velocity = Vector2.zero;
        StopAllCoroutines();
    }
    
}
