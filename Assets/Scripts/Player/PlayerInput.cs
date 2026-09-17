using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    private Player_Actions _actions;
    public Vector2 moveAxis { get; private set; }
    public Vector2 lookAxis { get; private set; }
    bool jumpPerformed;
    bool runPerformed;
    public bool crouchPressed { get; private set; }

    public bool CheckJump()
    {
        if (!jumpPerformed) return false;
        else
        {
            jumpPerformed = false;
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

        _actions.Gameplay.Jump.performed += ToggleJump;

        _actions.Gameplay.Crouch.started += _ => crouchPressed = true;
        _actions.Gameplay.Crouch.canceled += _ => crouchPressed = false;
    }

    void ToggleJump(InputAction.CallbackContext context)
    {
        jumpPerformed = true;
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
        _actions.Gameplay.Move.performed -= PerformMove;

        _actions.Gameplay.Jump.performed -= ToggleJump;

        _actions.Gameplay.Run.performed -= ToggleRun;

        _actions.Gameplay.LookAround.performed -= PerformRotate;
        _actions.Gameplay.LookAround.canceled -= PerformRotate;

        _actions.Gameplay.Crouch.started -= _ => crouchPressed = true;
        _actions.Gameplay.Crouch.canceled -= _ => crouchPressed = false;
    }
}
