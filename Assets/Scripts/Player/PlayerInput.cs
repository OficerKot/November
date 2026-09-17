using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    private Player_Actions _actions;
    public Vector2 moveAxis { get; private set; }
    bool jumpPerformed;
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

    void Awake()
    {
        _actions = new Player_Actions();
    }

    void OnEnable()
    {
        _actions.Gameplay.Enable();

        _actions.Gameplay.Move.performed += PerformMove;
        _actions.Gameplay.Move.canceled += PerformMove;

        _actions.Gameplay.Jump.performed += PerformJump;

        _actions.Gameplay.Crouch.started += _ => crouchPressed = true;
        _actions.Gameplay.Crouch.canceled += _ => crouchPressed = false;
    }

    void PerformJump(InputAction.CallbackContext context)
    {
        jumpPerformed = true;
    }

    void PerformMove(InputAction.CallbackContext context)
    {
        moveAxis = context.ReadValue<Vector2>();
    }


    void OnDisable()
    {
        _actions.Gameplay.Move.performed -= PerformMove;
        _actions.Gameplay.Jump.performed -= PerformJump;

        _actions.Gameplay.Crouch.started -= _ => crouchPressed = true;
        _actions.Gameplay.Crouch.canceled -= _ => crouchPressed = false;
    }
}
