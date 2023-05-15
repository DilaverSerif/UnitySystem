using UnityEngine;

public abstract class Event : ScriptableObject
{
    protected EventListener EventListener;
    
    // public void Subscribe()
    // {
    //     listener.AddListener(Action);
    //     EventListener = listener;
    // }
    
    public void Unsubscribe()
    {
        EventListener.RemoveListener(Action);
        EventListener = null;
    }
    public abstract void Action();
}