using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    private Player_Actions _actions;
    public Vector2 moveAxis { get; private set; }
    public Vector2 lookAxis { get; private set; }

    public bool IsJumpHeld => _actions.Gameplay.Jump.IsPressed();
    bool runPerformed;
    bool crouchPerformed;
    public bool CheckCrouch()
    {
        if (!crouchPerformed) return false;
        else
        {
            crouchPerformed = false;
            return true;
        }
    }

    public bool CheckRun()
    {
        if (!runPerformed) return false;
        else
        {
            runPerformed = false;
            return true;
        }
    }

    void Awake()
    {
        _actions = new Player_Actions();
    }

    void OnEnable()
    {
        _actions.Gameplay.Enable();

        _actions.Gameplay.LookAround.performed += PerformRotate;
        _actions.Gameplay.LookAround.canceled += PerformRotate;

        _actions.Gameplay.Run.performed += ToggleRun;

        _actions.Gameplay.Move.performed += PerformMove;
        _actions.Gameplay.Move.canceled += PerformMove;

        _actions.Gameplay.Crouch.started += _ => crouchPerformed = true;
        _actions.Gameplay.Crouch.canceled += _ => crouchPerformed = false;
    }

    void ToggleRun(InputAction.CallbackContext context)
    {
        runPerformed = true;
    }

    void PerformMove(InputAction.CallbackContext context)
    {
        moveAxis = context.ReadValue<Vector2>();
    }

    void PerformRotate(InputAction.CallbackContext context)
    {
        lookAxis = context.ReadValue<Vector2>();
    }

    void OnDisable()
    {
        _actions.Gameplay.Disable();

        _actions.Gameplay.Move.performed -= PerformMove;
        _actions.Gameplay.Move.canceled -= PerformMove;

        _actions.Gameplay.Run.performed -= ToggleRun;

        _actions.Gameplay.LookAround.performed -= PerformRotate;
        _actions.Gameplay.LookAround.canceled -= PerformRotate;

        _actions.Gameplay.Crouch.started -= _ => crouchPerformed = true;
        _actions.Gameplay.Crouch.canceled -= _ => crouchPerformed = false;
    }
}
