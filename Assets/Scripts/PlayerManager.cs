using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerManager : MonoBehaviour
{
    private void Awake()
    {
        GameManager.SetSpawnPoint(transform);
        InitStates();
    }
    private void Update()
    {
        if (_stateMachine != null)
            _stateMachine.Update();
    }

    /// <summary>
    /// Freeze the player character (during camera transitions)
    /// </summary>
    public void DeactivatePlayer()
    {
        if ((_rigidbody ?? (_rigidbody = GetComponent<Rigidbody2D>())) == null)
        {
            Debug.LogWarning("PlayerManager: Rigidbody2D not provided");
            return;
        }
        if ((_animator ?? (_animator = GetComponent<Animator>())) == null)
        {
            Debug.LogWarning("PlayerManager: Animator not provided");
            return;
        }
        _animator.speed = 0;
        _storedVelocity = _rigidbody.velocity;
        _rigidbody.constraints = RigidbodyConstraints2D.FreezeAll;
        if (_stateMachine != null)
            _stateMachine.Goto("InactiveState");
    }
    /// <summary>
    /// Reactivates player character (after camera transition)
    /// </summary>
    public void ReactivatePlayer()
    {
        if ((_rigidbody ?? (_rigidbody = GetComponent<Rigidbody2D>())) == null)
        {
            Debug.LogWarning("PlayerManager: Rigidbody2D not provided");
            return;
        }
        if ((_animator ?? (_animator = GetComponent<Animator>())) == null)
        {
            Debug.LogWarning("PlayerManager: Animator not provided");
            return;
        }
        _animator.speed = 1;
        _rigidbody.constraints = RigidbodyConstraints2D.FreezeRotation;
        _rigidbody.velocity = _storedVelocity;

        if (_stateMachine != null)
            _stateMachine.Goto("ActiveState");
    }

    private StateMachine _stateMachine;
    private void InitStates()
    {
        _stateMachine = new StateMachine();
        _stateMachine.Add("ActiveState", ActiveState);
        _stateMachine.Add("InactiveState", InactiveState);

        _stateMachine.Goto("ActiveState");
    }
    private void ActiveState()
    {
        SetMovement();
        SetAnimatorState();
    }
    private Vector2 _storedVelocity;
    private void InactiveState()
    {
        if ((_rigidbody ?? (_rigidbody = GetComponent<Rigidbody2D>())) == null)
        {
            Debug.LogWarning("PlayerManager: Rigidbody2D not provided");
            return;
        }
        _rigidbody.constraints = RigidbodyConstraints2D.FreezeAll;
    }

    [SerializeField] private float _MovementSpeed = 6f;
    [SerializeField] private float _JumpForce = 12f;
    [SerializeField] private float _GravityEnforcer = 2.5f;
    [SerializeField] private float _TerminalVelocity = 24f;

    private Rigidbody2D _rigidbody;
    /// <summary>
    /// React to User input, and grounding checks to manage movement
    /// </summary>
    private void SetMovement()
    {
        if ((_rigidbody ?? (_rigidbody = GetComponent<Rigidbody2D>())) == null)
        {
            Debug.LogWarning("PlayerManager: Rigidbody2D not provided");
            return;
        }

        if (Input.GetButtonDown("Jump") && IsGrounded())
            _rigidbody.velocity = (Vector2.up * _JumpForce);

        if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) > 0)
        {
            _rigidbody.constraints = RigidbodyConstraints2D.FreezeRotation;
            _rigidbody.velocity = new Vector2(_MovementSpeed * Input.GetAxisRaw("Horizontal"), _rigidbody.velocity.y);
        }
        else
        {
            _rigidbody.velocity = new Vector2(0, _rigidbody.velocity.y);
            _rigidbody.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
        }

        if(!IsGrounded())
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, Mathf.Max(-_TerminalVelocity, _rigidbody.velocity.y - (_GravityEnforcer * Time.deltaTime)));
    }

    [SerializeField] private LayerMask _GroundLayers;
    [SerializeField] private Vector2 _GroundBoxOffset;
    [SerializeField] private Vector2 _GroundBoxSize = Vector2.one;
    private bool IsGrounded()
    {
        RaycastHit2D rcHit = Physics2D.BoxCast((Vector2)transform.position + _GroundBoxOffset, _GroundBoxSize, 0f, Vector2.down, 0, _GroundLayers);
        return rcHit.collider != null;
    }

    private Animator _animator;
    /// <summary>
    /// Manages the animations of the Player Character
    /// </summary>
    private void SetAnimatorState()
    {
        if ((_animator ?? (_animator = GetComponent<Animator>())) == null)
        {
            Debug.LogWarning("PlayerManager: Animator not provided");
            return;
        }
        _animator.SetBool("IsGrounded", IsGrounded());
        _animator.SetFloat("Speed", Mathf.Abs(Input.GetAxisRaw("Horizontal")));
        if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) > 0)
            transform.localScale = new Vector3(Input.GetAxisRaw("Horizontal"), 1, 1);
    }

    private void OnDrawGizmosSelected()
    {
        if (IsGrounded()) Gizmos.color = Color.green;
        else Gizmos.color = Color.red;

        Gizmos.DrawWireCube((Vector2)transform.position + _GroundBoxOffset, new Vector3(_GroundBoxSize.x, _GroundBoxSize.y, 0));
    }
}
