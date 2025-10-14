using UnityEngine;
using UnityEngine.InputSystem;
using Weapon;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private Animator _animator;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Collider2D _weaponCollider;
    [SerializeField] private float _speed = 6f;
    [SerializeField] private float _jumpForce = 10f;
    [SerializeField] private Transform _groundCheck;
    [SerializeField] private float _groundCheckRadius = 0.15f;
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private PlayerWeapon _playerWeapon;

    //
    [SerializeField] private float _maxJumpForce = 20f;
    [SerializeField] private float _jumpChargeTime = 0.5f;
    private bool _isHolding;
    private float _jumpHoldTime;
    //
    
    private Vector2 _movement;
    private bool _isGrounded;
    private string[] attacks = { "Attack1", "Attack2" };
    
    public bool IsAttacking { get; set; }
    public bool IsHurt { get; set; }
    
    
    private void FixedUpdate()
    {
        HandleMovement();
        CheckGroundedState();
        HandleJumpCharge();
    }

    private void HandleMovement()
    {
        if (IsHurt)
        {
            _rb.velocity = new Vector2(0f, _rb.velocity.y);
        }
        else
        {
            _rb.velocity = new Vector2(_movement.x * _speed, _rb.velocity.y);
        }
        _animator.SetFloat("Speed", Mathf.Abs(_movement.x));
    }

    private void CheckGroundedState()
    {
        _isGrounded = Physics2D.OverlapCircle(_groundCheck.position, _groundCheckRadius, _groundLayer);
        _animator.SetBool("IsGrounded", _isGrounded);
        _animator.SetFloat("VSpeed", _rb.velocity.y);
    }

    public void Move(InputAction.CallbackContext ctx)
    {       
        _movement = ctx.ReadValue<Vector2>(); 
        if (_movement.x != 0) 
        { 
            _spriteRenderer.flipX = _movement.x < 0;
        }
        if (_spriteRenderer.flipX)
        {
            _weaponCollider.offset = new Vector2(-_weaponCollider.offset.x, _weaponCollider.offset.y);
        }
        else
        {
            _weaponCollider.offset = new Vector2(Mathf.Abs(_weaponCollider.offset.x), _weaponCollider.offset.y);
        }
    }

    // public void Jump(InputAction.CallbackContext ctx)
    // {
    //     if (ctx.performed && _isGrounded && !IsHurt)
    //     {
    //         _rb.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
    //         _animator.SetTrigger("Jump");
    //     }
    // }

    private void HandleJumpCharge()
    {
        if (_isHolding)
        {
            _jumpHoldTime += Time.deltaTime;
        }
    }

    public void Jump(InputAction.CallbackContext ctx)
    {
        if (ctx.started && _isGrounded && !IsHurt)
        {
            _isHolding = true;
            _jumpHoldTime = 0f;
            _animator.SetTrigger("Jump");
            _rb.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
        }
        else if (ctx.canceled && _isHolding && !_isGrounded && _jumpHoldTime >= _jumpChargeTime)
        {
            float appliedJumpForce = _maxJumpForce - _jumpForce;
            _rb.velocity = Vector2.zero;
            _rb.AddForce(Vector2.up * appliedJumpForce, ForceMode2D.Impulse);
            _isHolding = false;
        }
        else if (ctx.canceled && _isHolding)
        {
            _isHolding = false;
        }
    }

    public void Attack(InputAction.CallbackContext ctx)
    {
        if (ctx.performed && !IsAttacking && !IsHurt && _isGrounded)
        {
            _animator.SetTrigger(attacks[Random.Range(0, attacks.Length)]);
            IsAttacking = true;
        }
    }

    public void OnAttackEnd()
    {
        IsAttacking = false;
        _playerWeapon.DisableCollider();
    }

    public void OnHurtEnd()
    {
        IsHurt = false;
    }
}
