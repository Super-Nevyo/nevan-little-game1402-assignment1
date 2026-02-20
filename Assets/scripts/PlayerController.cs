using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D _playerRB;
    [SerializeField] private float moveSpeed = 0.5f; // the acceleration from pressing the arrow keys
    [SerializeField] private float jumpForce = 20f; // the force applied when pressing the up arrow key or space key
    [SerializeField] private float gravity = 4f;
    [SerializeField] private float gravityChangeAmount = 2f;
    [SerializeField] private float positionChangeReductionFromVelocity = 120f;

    [SerializeField] private InputManager inputManager;
    private bool _canJump => _isGrounded || (Time.time < _lastGroundedCheck + _coyoteTime && Time.time > _lastJumpTime + _coyoteTime); // implementing coyote time and cutting it if you have jumped
    private float _lastGroundedCheck;
    [SerializeField] private float _coyoteTime = 0.3f; // the amount of time after being grounded that jumping is still allowed for
    private float _lastJumpTime;
    [Header("Ground Check")] 
    [SerializeField] private LayerMask groundLayer; // the layer detected by the ray cast for ground check
    [SerializeField] private Vector2 startPointOffSet;
    [SerializeField] private float groundCheckDistance;
    private float _horizontalInputVelocity; // the axis measured for left and right arrow key presses
    private float _positionChangeDirection; // the axis measured for a and d key presses
    private float _gravityChangeDirection; // the axis measured for w and s key presses
    private bool _isGrounded = false;
    
    
    void Awake()
    {
        _playerRB = GetComponent<Rigidbody2D>(); 
    }

    void OnEnable() 
    {
        // listening to the input manager for the key presses
        inputManager.OnJump += HandleJump;
        inputManager.OnMove += HandleMove;
        inputManager.ChangePosition += PositionInput;
        inputManager.ChangeGravity += GravityInput;
    }
    void OnDisable() 
    {
        // stopping listening to the input manager
        inputManager.OnJump -= HandleJump;
        inputManager.OnMove -= HandleMove;
        inputManager.ChangePosition -= PositionInput;
        inputManager.ChangeGravity -= GravityInput;
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

    // talking the left and right arrow key inputs and turns it into a variable to be used in the fixed update function as a part of the handle movement function
    private void HandleMove(float value)
    { 
        _horizontalInputVelocity = value;
    }
    // taking the variable from the function above and using it in the fixed update function to change velocity
    void HandleMovement()
    {
        if (_playerRB == null) return;
        _playerRB.linearVelocityX += _horizontalInputVelocity * moveSpeed;
    }
    // taking the a and d key inputs and turns it into a variable to be used in the position changing
    void PositionInput(float value)
    {
        _positionChangeDirection = value;
    }
    // taking the variable from the function above and uses it in the fixed update to change position based on velocity
    void HandlePosition()
    {
        if (_playerRB == null) return;
        transform.position = new Vector2(_positionChangeDirection * Mathf.Abs(_playerRB.linearVelocityX) / positionChangeReductionFromVelocity + transform.position.x, transform.position.y);
    }
    // takes the input from w and s and changes gravity
    void GravityInput(float value)
    {
        _playerRB.gravityScale = -value * gravityChangeAmount + gravity;
    }
    // using a ray cast to check for the ground to allow jumping
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
