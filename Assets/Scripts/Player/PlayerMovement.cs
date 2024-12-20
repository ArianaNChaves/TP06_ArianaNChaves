using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerMovement : MonoBehaviour
{
    private static readonly int MoveSpeed = Animator.StringToHash("MoveSpeed");
    private static readonly int JumpVelocity = Animator.StringToHash("JumpVelocity");
    private static readonly int IsGrounded = Animator.StringToHash("isGrounded");

    [Header("Scripts References")]
    [SerializeField] private EntitySO entityData;
    
    [SerializeField] private LayerMask jumpLayer; 
    [SerializeField] private Transform feetPosition;
    [SerializeField] private Vector2 collisionBoxSize;
    [SerializeField] private float jumpDelayTime = 0.4f;

    [SerializeField] private Transform body;

    private Rigidbody2D _rigidbody2D;
    private Animator _animator;
    private int _maxJumps;
    private int _currentJumps = 0;
    private bool _isGrounded;
    private bool _canJump = true;
    private float _horizontalMovement;
    private const float NormalSpeed = 1.0f;
    private const float AirSpeedModifier = 0.5f;
    private float _jumpTimer = 0;
    private bool _isFalling = false;
    private bool _isFacingRight = true;
    private void Start()
    {
        _animator = GetComponentInChildren<Animator>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _maxJumps = entityData.StartJumpsAmount;
    }

    private void FixedUpdate()
    {
        Move();
        CheckGrounded(); 
    }

    private void Update()
    {
        JumpHandler();
    }

    private void Move()
    {
        _horizontalMovement = Input.GetAxis("Horizontal");
        _horizontalMovement *= _isGrounded ? NormalSpeed : AirSpeedModifier;
        _animator.SetFloat(MoveSpeed, Mathf.Abs(_horizontalMovement));
        if (_horizontalMovement > 0)
        {
            Utilities.RotateObjectOnMovement(_horizontalMovement, ref _isFacingRight, ref body);
        }
        else if (_horizontalMovement < 0)
        {
            Utilities.RotateObjectOnMovement(_horizontalMovement, ref _isFacingRight, ref body);

        }
        
        Vector2 speed = new Vector2(_horizontalMovement * (entityData.MovementSpeed), _rigidbody2D.velocity.y);
        _rigidbody2D.velocity = speed;
        
    }


    private void JumpDelay()
    {
        if (_canJump) return;
        _jumpTimer += Time.deltaTime;
        
        if (_jumpTimer >= jumpDelayTime)
        {
            _canJump = true;
        }
    }
    
    private void JumpHandler()
    {
        JumpDelay();
        _animator.SetFloat(JumpVelocity, _rigidbody2D.velocity.y);
        if (_rigidbody2D.velocity.y <= 0)
        {
            _isFalling = true;
        }
        if (Input.GetKeyDown(KeyCode.Space) && _canJump && _currentJumps < _maxJumps)
        {
            _currentJumps++;
            _canJump = false;
            _jumpTimer = 0;
            _isFalling = false;
            Jump();
        }
        
    }
    private void Jump()
    {
        AudioManager.Instance.PlayEffect("Jump");
        _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, 0);
        _rigidbody2D.AddForce(Vector2.up * entityData.JumpForce, ForceMode2D.Impulse);
    }


    private void CheckGrounded()
    {
        Collider2D hit = Physics2D.OverlapBox(feetPosition.position, collisionBoxSize, 0, jumpLayer);
        _isGrounded = hit != null;
        _animator.SetBool(IsGrounded, _isGrounded);
        if (_isGrounded && _isFalling)
        {
            _currentJumps = 0;
        }
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(feetPosition.position, collisionBoxSize);
    }
    
    public void AddMaxJumps()
    {
        this._maxJumps++;
    }

    public int GetMaxJumps()
    {
        return this._maxJumps;
    }
}
