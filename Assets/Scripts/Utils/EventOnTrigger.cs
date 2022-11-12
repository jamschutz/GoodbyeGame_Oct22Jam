using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventOnTrigger : MonoBehaviour
{
    public UnityEvent eventTrigger;
    public string targetTag = "Player";
    public bool onlyOnce = true;


    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == targetTag) {
            eventTrigger.Invoke();

            if(onlyOnce) this.enabled = false;
        }
    }
    
}
