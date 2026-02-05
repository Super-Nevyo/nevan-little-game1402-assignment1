using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    
    private PlayerInputActions _playerInputActions;

    public System.Action OnJump;
    public System.Action<float> OnMove; // the left and right arrow keys
    public System.Action<float> ChangePosition; // the a and d keys
    public System.Action<float> ChangeGravity; // the w and s keys
    void Awake()
    {
        _playerInputActions = new PlayerInputActions();
        _playerInputActions.Enable();
    }

    void OnEnable()
    {
        _playerInputActions.player.Jump.performed += OnJumpPressed;
        
    }

    void OnDisable()
    {
        _playerInputActions.player.Jump.performed -= OnJumpPressed;
    }

    void OnJumpPressed(InputAction.CallbackContext ctx)
    {
        OnJump?.Invoke();
    }

    void OnMovement()
    {
        OnMove?.Invoke(_playerInputActions.player.VelocityChange.ReadValue<float>());
    }
    void OnChangePosition()
    {
        ChangePosition?.Invoke(_playerInputActions.player.PositionChange.ReadValue<float>());
    }
    void OnChangeGravity()
    {
        ChangeGravity?.Invoke(_playerInputActions.player.GravityControl.ReadValue<float>());
    }

    private void Update()
    {
        OnMovement(); // tells the player what direction they should be accelerating in
        OnChangePosition(); // tells the player what direction they should be adding or subtracting movement from
        OnChangeGravity(); // tells the player if they should increase or decrease gravity
    }
}
