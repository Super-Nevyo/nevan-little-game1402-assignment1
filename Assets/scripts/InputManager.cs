using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    
    private PlayerInputActions _playerInputActions;

    public System.Action OnJump;
    public System.Action<float> OnMove;
    public System.Action<float> ChangePosition;
    public System.Action<float> ChangeGravity;
    void Awake()
    {
        _playerInputActions = new PlayerInputActions();
        _playerInputActions.Enable();
    }

    void OnEnable()
    {
        _playerInputActions.player.Jump.performed += OnJumpPressed;
        //_playerInputActions.player.LeftRight.performed += OnMovement;
        
    }

    void OnDisable()
    {
        _playerInputActions.player.Jump.performed -= OnJumpPressed;
        //_playerInputActions.player.LeftRight.performed -= OnMovement;
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
        OnMovement();
        OnChangePosition();
        OnChangeGravity();
    }
}
