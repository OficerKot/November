using System;
using System.Collections.Generic;

public interface IReadOnlyEventBus
{
    public void Subscribe<T>(Action<T> handler);
    public void Unsubscribe<T>(Action<T> handler);
}

public interface IEventBus
{
    public void Publish<T>(T eventData);
}
public class EventBus : IReadOnlyEventBus, IEventBus
{
    private readonly Dictionary<Type,Delegate> handlers = new();

    public void Subscribe<T>(Action<T> handler)
    {
        Type eventType = typeof(T);
        
        if (handlers.TryGetValue(eventType, out Delegate existing))
        {
            handlers[eventType] = Delegate.Combine(existing, handler);
        }
        else
        {
            handlers[eventType] = handler;
        }
    }

    public void Unsubscribe<T>(Action<T> handler)
    {
        Type eventType = typeof(T);
        if (!handlers.TryGetValue(eventType, out Delegate existing)) return;
        
        Delegate updated = Delegate.Remove(existing, handler);
        if (updated == null) handlers.Remove(eventType);
        else handlers[eventType] = updated;
    }

    public void Publish<T>(T eventData)
    {
        if (handlers.TryGetValue(typeof(T), out Delegate handler))
        {
            ((Action<T>)handler).Invoke(eventData);
        }
    }
}
    