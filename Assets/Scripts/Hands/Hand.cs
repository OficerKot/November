using System;
using Hands;
using UnityEngine;

public class Hand : MonoBehaviour
{
    private bool isActive;
    private IGrabable currentGrabable;
    private IHookable currentHookable;

    public void Toggle(bool isActive)
    {
        if (isActive == this.isActive) return;
        
        if(isActive) Activate();
        else Deactivate();
      
        this.isActive =  isActive;
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
