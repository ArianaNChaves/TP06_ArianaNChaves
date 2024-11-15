using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private static readonly int IsDead = Animator.StringToHash("isDead");

    [Header("References")]
    [SerializeField] private EntitySO entityData;
    
    [Header("Patrol")]
    [SerializeField] private Transform positionA;
    [SerializeField] private Transform positionB;
    [SerializeField] private Transform body;
    [SerializeField] private float maxDistance;
    [SerializeField] private float patrolWaitTime;
    [SerializeField] private bool isFacingRight;

    [Header("Combat")] 
    [SerializeField] private Transform target;
    [SerializeField] private LayerMask targetLayer;
    [SerializeField] private float hitRadius;
    [SerializeField] private Transform hitPoint;


    private State _state;
    private Rigidbody2D _rigidbody2D;
    private Animator _animator;
    private bool _isMovingToB;
    private bool _isWaiting = false;
    private bool _isChangingState = false;
    private bool _isAttacking = false;
    private float _timer = 0;
    private IHealthHandler _healthHandler;
    public enum State
    {
        Patrol,
        Attack,
        Death,
    }

    private void Start()
    {
        _animator = GetComponentInChildren<Animator>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _state = State.Patrol;
    }

    private void Update()
    {
        if (_animator.GetBool(IsDead)) return;
        UpdateBodyRotation();
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
            case State.Attack:
            {
                InitiateCombat();
                break;
            }
        }
    }

    private void Patrol()
    {
        if (_isWaiting) return;
        Vector2 targetX = new Vector2(_isMovingToB ? positionB.position.x : positionA.position.x, transform.position.y);
        MovingTo(targetX);
        
        if (Mathf.Abs(transform.position.x - targetX.x) < maxDistance && !_isWaiting)
        {
            StartCoroutine(ChangeDirection());
        }
    }

    private void MovingTo(Vector2 targetDirection)
    {
        Vector2 direction = (targetDirection - (Vector2)transform.position).normalized;
        _rigidbody2D.velocity = new Vector2(direction.x * entityData.MovementSpeed, transform.position.y);
    }

    private IEnumerator ChangeDirection()
    {
        _rigidbody2D.velocity = Vector2.zero;
        _isWaiting = true;
        yield return new WaitForSeconds(patrolWaitTime); 
        _isMovingToB = !_isMovingToB;
        _isWaiting = false;
    }

    private void InitiateCombat()
    {
        if(!target) return;
        _timer += Time.deltaTime;
        Vector2 targetX = new Vector2(target.position.x, transform.position.y);
        MovingTo(targetX);
        if (Mathf.Abs(transform.position.x - targetX.x) < maxDistance)
        {
            _rigidbody2D.velocity = Vector2.zero;
        }

        if (_healthHandler == null) return;
        if (_isAttacking && _timer >= entityData.AttackRate)
        {
            _timer = 0;
            _healthHandler.UpdateHealth(-entityData.Damage);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        _healthHandler = collision.gameObject.GetComponent<IHealthHandler>();
        if (_healthHandler == null) return;
        _isAttacking = true;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _isAttacking = false;
        }
    }
    
    private void UpdateBodyRotation()
    {
        Utilities.RotateObjectOnMovement(_rigidbody2D.velocity.x,ref isFacingRight, ref body);
    }
    
    public void ChangingStateTo(State state)
    {
        _isChangingState = true;
        _rigidbody2D.velocity = Vector2.zero;
        StopAllCoroutines();
        _state = state;
        _isWaiting = false;
        _isAttacking = false;
        _isChangingState = false;
    }
    
}
