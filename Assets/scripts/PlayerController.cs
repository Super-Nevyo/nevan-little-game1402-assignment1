using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D _playerRB;
    [SerializeField] private float moveSpeed = 0.5f;
    [SerializeField] private float jumpForce = 20f;

    [SerializeField] private InputManager inputManager;
    private bool _canJump => _isGrounded || (Time.time < _lastGroundedCheck + _coyoteTime && Time.time > _lastJumpTime + _coyoteTime);
    private float _lastGroundedCheck;
    [SerializeField] private float _coyoteTime = 0.3f;
    private float _lastJumpTime;
    [Header("Ground Check")] 
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Vector2 startPointOffSet;
    [SerializeField] private float groundCheckDistance;
    private float _horizontalInputVelocity;
    private float _positionChangeDirection;
    private float _gravityChangeDirection;
    private bool _isGrounded = false;
    
    
    void Awake()
    {
        _playerRB = GetComponent<Rigidbody2D>(); 
    }

    void OnEnable()
    {
        inputManager.OnJump += HandleJump;
        inputManager.OnMove += HandleMove;
        inputManager.ChangePosition += PositionInput;
        inputManager.ChangeGravity += GravityInput;
    }
    void OnDisable()
    {
        inputManager.OnJump -= HandleJump;
        inputManager.OnMove -= HandleMove;
        inputManager.ChangePosition -= PositionInput;
        inputManager.ChangeGravity += GravityInput;
    }

    void FixedUpdate()
    {
        HandleMovement();
        GroundCheck();
        HandlePosition();
    }

    private void HandleJump()
    {
        if (_playerRB == null) return;

        if (_canJump)
        {
            _playerRB.AddForceY(jumpForce, ForceMode2D.Impulse);
            _lastJumpTime = Time.time;
        }
    }

    private void HandleMove(float value)
    {
        _horizontalInputVelocity = value;
    }
    void HandleMovement()
    {
        if (_playerRB == null) return;
        _playerRB.linearVelocityX += _horizontalInputVelocity * moveSpeed;
    }
    void PositionInput(float value)
    {
        _positionChangeDirection = value;
    }
    void HandlePosition()
    {
        if (_playerRB == null) return;
        transform.position = new Vector2(_positionChangeDirection * Mathf.Abs(_playerRB.linearVelocityX) / 120f + transform.position.x, transform.position.y);
    }
    void GravityInput(float value)
    {
        _playerRB.gravityScale = -value * 2 + 4;
    }

    void GroundCheck()
    {
        _isGrounded = Physics2D.Raycast((Vector2)transform.position + startPointOffSet, Vector2.right, groundCheckDistance, groundLayer);
        if (_isGrounded)
        {
            _lastGroundedCheck = Time.time;
        }
    }

    void OnDrawGizmos()
    {
        Debug.DrawLine((Vector2)transform.position + startPointOffSet,(Vector2) transform.position + startPointOffSet + Vector2.right * groundCheckDistance, _isGrounded ? Color.green : Color.red);
    }
}
