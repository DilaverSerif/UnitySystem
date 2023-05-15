using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Event System/Create an EventListener", fileName = "Event", order = 0)]
public class EventListener : ScriptableObject
{
    public UnityEvent EventAction;
    
    public void Invoke()
    {
        EventAction?.Invoke();
    }
    
    public void AddListener(UnityAction action)
    {
        if(action == null) return;
        EventAction.AddListener(action);
    }
    
    public void RemoveListener(UnityAction action)
    {
        if(action == null) return;
        if(EventAction.GetPersistentEventCount() == 0) return;
        EventAction.RemoveListener(action);
    }
}