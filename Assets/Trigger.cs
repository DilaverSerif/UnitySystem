using System;
using _SYSTEMS_._Interaction_System_.Abstract;
using _SYSTEMS_._InventorySystem_.Abstract;
using UnityEngine;
using System.Collections;
using _SYSTEMS_._Character_Controller_;

public class Trigger : MonoBehaviour,IUsable<PlayerController>
{
    public float time;
    public float MaxTime;
    public Bag bag;
    private Coroutine _countdownCoroutine;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Bag bag))
        {
            this.bag = bag;
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out Bag bag))
        {
            this.bag = null;
            if(_countdownCoroutine != null)
                StopCoroutine(_countdownCoroutine);
            _countdownCoroutine = StartCoroutine(Countdown());
        }
    }
    
    private IEnumerator Countdown()
    {
        while (time > 0)
        {
            yield return new WaitForSeconds(1);
            time--;
        }
        
        _countdownCoroutine = null;
    }

    public void Use()
    {
        time += Time.deltaTime;
        if (time >= MaxTime)
        {
            Debug.Log("Trigger");
            time = 0;
        }

    }

    public void StopUse()
    {
        
    }

    public void Use(PlayerController target)
    {
        throw new NotImplementedException();
    }

    public void StopUse(PlayerController target)
    {
        throw new NotImplementedException();
    }
}
