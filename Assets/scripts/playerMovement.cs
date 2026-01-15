using UnityEngine;
using UnityEngine.InputSystem;

public class playerMovement : MonoBehaviour
{


    private PlayerInputActions _testActions;
    private Rigidbody2D rb;
    public float JumpForce = 10;
    public float MoveSpeed = 30;
    private float horizontalMove;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); 
    }
    void Awake()
    {
        _testActions = new PlayerInputActions(); // created and object
        _testActions.Enable(); // we turn it on to listen to key inputs
    }

    void OnEnable()
    {
        _testActions.player.Jump.performed += Jump;
    }

    void OnDisable()
    {
        _testActions.player.Jump.performed -= Jump;
    }

    void Jump(InputAction.CallbackContext ctx)
    {
        Debug.Log("Jump");
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, JumpForce);
    }

    void Update()
    {
        if (_testActions.player.LeftRight.IsPressed())
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x + (_testActions.player.LeftRight.ReadValue<float>() * MoveSpeed), rb.linearVelocity.y);
        }
    }

    

}
