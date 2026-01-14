using UnityEngine;
using UnityEngine.InputSystem;

public class playerMovement : MonoBehaviour
{


    private PlayerInputActions _testActions;
    public Rigidbody2D rb;
    public float JumpForce = 10;

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
        rb.linearVelocity = new Vector2(0, JumpForce);
    }
    
}
