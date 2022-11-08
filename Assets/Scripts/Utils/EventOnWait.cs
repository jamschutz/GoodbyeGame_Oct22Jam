using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventOnWait : MonoBehaviour
{
    public UnityEvent eventTrigger;
    public float waitTime;
    public bool triggerOnAwake = true;


    void Start()
    {
        if(triggerOnAwake) {
            Invoke("TriggerEvent", waitTime);
        }
    }


    public void TriggerEvent()
    {
        eventTrigger.Invoke();
    }
    
}
