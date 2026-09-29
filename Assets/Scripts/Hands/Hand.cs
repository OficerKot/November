using System;
using Hands;
using UnityEngine;

public class Hand : MonoBehaviour
{
    private bool _isActive;
    public bool IsActive => _isActive;
    
    private IGrabable currentGrabable;
    private IHookable currentHookable;
    public bool IsHooking => currentHookable != null;
    private Vector3 startPos;
    public Vector3 StartPost => startPos;

    private void Awake()
    {
        startPos =  transform.localPosition;
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
