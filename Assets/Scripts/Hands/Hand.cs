using System;
using Climbing;
using Hands;
using UnityEngine;

public class Hand : MonoBehaviour
{
    private bool _isActive;
    public bool IsActive => _isActive;
    public bool IsHooking => currentHookable != null;
    public bool IsFalling => handStamina.CurStamina <= 0;
    public bool CanHook => handStamina.CurStamina > minStaminaToHook;
    
    private IGrabable currentGrabable;
    private IHookable currentHookable;
    private float minStaminaToHook;
    private Stamina handStamina;

    public void Init(float defaultStamina, float minStaminaToHook)
    {
        handStamina = new Stamina(defaultStamina);
        this.minStaminaToHook = minStaminaToHook;
        
    }
    public void RecoverStamina(float val, float deltaTime)
    {
        handStamina.Recover(val, deltaTime);
    }

    public void DrainStamina(float val, float deltaTime)
    {
        handStamina.Drain(val, deltaTime);
        Debug.Log("Draining stamina. Cur stamina: " + handStamina.CurStamina);
    }

    public void Toggle(bool isActive)
    {
        if (isActive == this._isActive) return;
        
        if(isActive) Activate();
        else Deactivate();
      
        this._isActive =  isActive;
    }
    void Activate()
    {
        Debug.Log($"Hand {gameObject.name} has been activated");
    }

    void Deactivate()
    {
        Debug.Log($"Hand {gameObject.name} has been deacitvated");
    }

    public void GrabItem(IGrabable grabable)
    {
        Release();
        currentGrabable = grabable;
    }

    public void Hook(IHookable hookable)
    {
        Release();
        currentHookable = hookable;
        Debug.Log($"Hand is on {hookable}");
    }

    public void Release()
    {
        currentGrabable = null;
        currentHookable = null;
    }
}
