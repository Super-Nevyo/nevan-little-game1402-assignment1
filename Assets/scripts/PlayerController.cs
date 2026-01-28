using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D _playerRB;
    [SerializeField] private float moveSpeed = 0.5f;
    [SerializeField] private float jumpForce = 20f;

    [SerializeField] private InputManager inputManager;
    private bool _canJump => _isGrounded || Time.time < _lastGroundedCheck + _coyoteTime;
    [SerializeField] private float _lastGroundedCheck;
    [SerializeField] private float _coyoteTime = 0.3f;
    [Header("Ground Check")] 
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Vector2 startPointOffSet;
    [SerializeField] private float groundCheckDistance;
    private float _horizontalInput = 0;
    private bool _isGrounded = false;
    
    
    void Awake()
    {
        _playerRB = GetComponent<Rigidbody2D>(); 
    }

    void OnEnable()
    {
        inputManager.OnJump += HandleJump;
        inputManager.OnMove += HandleMove;
    }
    void OnDisable()
    {
        inputManager.OnJump -= HandleJump;
        inputManager.OnMove -= HandleMove;
    }

    void FixedUpdate()
    {
        HandleMovement();
        GroundCheck();
    }

    private void HandleJump()
    {
        if (_playerRB == null) return;

        if (_canJump)
            _playerRB.AddForceY(jumpForce, ForceMode2D.Impulse);
    }

    private void HandleMove(float value)
    {
        _horizontalInput = value;
    }

    void HandleMovement()
    {
        if (_playerRB == null) return;
        _playerRB.linearVelocityX +=_horizontalInput * moveSpeed;
    }
    
    //void OnTriggerStay2D(Collider2D other)
    //{
    //    if (other.gameObject.CompareTag("Ground")) {_isGrounded = true;}
    //}
    
    //void OnTriggerExit2D(Collider2D other)
    //{
    //    if (other.gameObject.CompareTag("Ground")) {_isGrounded = false;}
    //}

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
