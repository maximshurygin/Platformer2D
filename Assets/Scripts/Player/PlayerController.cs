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
    [SerializeField] private LayerMask _bouncePadLayer;
    [SerializeField] private PlayerWeapon _playerWeapon;
    [SerializeField] private float _jumpForce = 10f;
    
    private bool _isHolding;
    private float _jumpHoldTime;
    private Vector2 _movement;
    private bool _isGrounded;
    private bool _isOnBouncePad;
    private string[] _attacks = { "Attack1", "Attack2" };
    private PlayerData _playerData;
    private HintManager _hintManager;
    private PauseManager _pauseManager;
    private bool _wasGrounded;
    private bool _doubleJumpPerformed;
    

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

    private void OnEnable()
    {
        IsAttacking = false;
    }
    
    private void FixedUpdate()
    {
        HandleMovement();
        CheckGroundedState();
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
        _isOnBouncePad = Physics2D.OverlapCircle(_groundCheck.position, _groundCheckRadius, _bouncePadLayer);
        _animator.SetBool("IsGrounded", _isGrounded);
        _animator.SetFloat("VSpeed", _rb.velocity.y);
        
        if (_isGrounded && !_wasGrounded)
        {
            _animator.ResetTrigger("Jump");
            _doubleJumpPerformed = false;
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
    

    public void Jump(InputAction.CallbackContext ctx)
    {
        if (_isOnBouncePad) return;
        
        if (ctx.started && !IsHurt && _isGrounded)
        {
            _animator.SetTrigger("Jump");
            _rb.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
        }
        else if (ctx.started && !IsHurt && !_isGrounded && !_doubleJumpPerformed)
        {
            _animator.SetTrigger("Jump");
            _rb.velocity = new Vector2(_rb.velocity.x, 0f);
            _rb.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
            _doubleJumpPerformed = true;
        }
    }
    

    public void Attack(InputAction.CallbackContext ctx)
    {
        if (ctx.performed && !IsAttacking && !IsHurt && _isGrounded)
        {
            _animator.SetTrigger(_attacks[Random.Range(0, _attacks.Length)]);
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
    
    public void ForceStop()
    {
        _rb.velocity = Vector2.zero;
        _animator.SetFloat("Speed", 0f);
        IsAttacking = false;
    }

}