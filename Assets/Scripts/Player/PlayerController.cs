using System;
using GameManagers;
using Player;
using UnityEngine;
using UnityEngine.InputSystem;
using Weapon;
using Zenject;
using Random = UnityEngine.Random;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private Animator _animator;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Collider2D _weaponCollider;
    [SerializeField] private float _speed = 6f;
    [SerializeField] private Transform _groundCheck;
    [SerializeField] private float _groundCheckRadius = 0.15f;
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private PlayerWeapon _playerWeapon;
    [SerializeField] private float _jumpForce = 10f;
    [SerializeField] private float _maxJumpForce = 20f;
    [SerializeField] private float _jumpChargeTime = 0.4f;
    [SerializeField] private float _coyoteTime = 0.2f;
    [SerializeField] private float _bufferTime = 0.2f;
    
    private bool _isHolding;
    private float _jumpHoldTime;
    private Vector2 _movement;
    private bool _isGrounded;
    private string[] attacks = { "Attack1", "Attack2" };
    private PlayerData _playerData;
    private HintManager _hintManager;
    private PauseManager _pauseManager;
    private bool _isUsingComputer;
    private bool _wasGrounded;

    private float _coyoteTimer;
    private float _jumpBufferTimer;
    private bool _jumpBuffered;

    public event Action OnInteract;
    
    public bool IsAttacking { get; set; }
    public bool IsHurt { get; set; }

    [Inject]
    public void Construct(PlayerData playerData, HintManager hintManager,  PauseManager pauseManager)
    {
        _playerData = playerData;
        _hintManager = hintManager;
        _pauseManager = pauseManager;
    }
    
    
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
        _wasGrounded = _isGrounded;

        _isGrounded = Physics2D.OverlapCircle(_groundCheck.position, _groundCheckRadius, _groundLayer);
        _animator.SetBool("IsGrounded", _isGrounded);
        _animator.SetFloat("VSpeed", _rb.velocity.y);
        
        if (_isGrounded && !_wasGrounded)
        {
            _animator.ResetTrigger("Jump");
            _jumpHoldTime = 0f;
            _isHolding = false;
        }
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
            _animator.SetTrigger("Jump");
            _rb.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
            _isHolding = true;
            _jumpHoldTime = 0f;
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

    public void Interact(InputAction.CallbackContext ctx)
    {
        if (ctx.performed && _playerData.IsComputerInRange)
        {
            _hintManager.HideHint();
            OnInteract?.Invoke();
        }
    }

    public void Pause(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            _pauseManager.TogglePause();
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
