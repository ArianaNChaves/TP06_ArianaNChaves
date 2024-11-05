using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerMovement : MonoBehaviour
{
    [Header("Scripts References")]
    [SerializeField] private PlayerSO playerData;
    [SerializeField] private PlayerRotation playerRotation;
    
    [SerializeField] private LayerMask jumpLayer; 
    [SerializeField] private Transform feetPosition;
    [SerializeField] private float sphereRadius;

    private Rigidbody2D _rigidbody2D;
    private bool _isGrounded;
    private float _horizontalMovement;
    private const float NormalSpeed = 1.0f;
    private const float AirSpeedModifier = 0.5f;
    private void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        Move();
        CheckGrounded(); 
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && _isGrounded)
        {
            Jump();
        }
    }

    private void Move()
    {
        _horizontalMovement = Input.GetAxis("Horizontal");
        _horizontalMovement *= _isGrounded ? NormalSpeed : AirSpeedModifier;
        
        if (_horizontalMovement > 0)
        {
            playerRotation.FlipPlayer(_horizontalMovement);
        }
        else if (_horizontalMovement < 0)
        {
            playerRotation.FlipPlayer(_horizontalMovement);
        }
        
        Vector2 speed = new Vector2(_horizontalMovement * (playerData.MovementSpeed), _rigidbody2D.velocity.y);
        _rigidbody2D.velocity = speed;
    }

    private void Jump()
    {
        // AudioManager.Instance.PlayEffect("Jump");
        _rigidbody2D.AddForce(Vector2.up * playerData.JumpForce, ForceMode2D.Impulse);
    }

    private void CheckGrounded()
    {
        Collider2D hit = Physics2D.OverlapCircle(feetPosition.position, sphereRadius, jumpLayer);
        _isGrounded = hit != null;
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(feetPosition.position, sphereRadius);
    }
   
   
}
