using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[DefaultExecutionOrder(0)]
public class EventManager : MonoBehaviour
{
    private IEnumerable<IEvent> _events;
    private void Awake()
    {
        var eventListeners = FindObjectsOfType<EventListener>();

        _events = FindObjectsOfType<MonoBehaviour>().OfType<IEvent>();
        
        foreach (var _event in _events)
        {
            _event.Subscribe();
        }
    }


    private void OnDisable()
    {
        foreach (var _event in _events)
        {
            _event.Unsubscribe();
        }
        
        _events.ToList().Clear();
    }
}
