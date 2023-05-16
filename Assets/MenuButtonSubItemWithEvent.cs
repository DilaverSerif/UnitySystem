using UnityEngine;
public class MenuButtonSubItemWithEvent : MenuSubItem, IEvent
{
    public EventListener onClickListener;
    public Event[] onClickEvents;
    
    public void Action()
    {
        Debug.Log("MenuButtonSubItem Action");
    }

    public void Subscribe()
    {
        onClickListener.AddListener(Action);
    }

    public void Unsubscribe()
    {
        onClickListener.RemoveListener(Action);
        onClickListener = null;
    }
}