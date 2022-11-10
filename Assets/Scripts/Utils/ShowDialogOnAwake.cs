using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

using DialogController = UI.Controllers.DialogController;

public class ShowDialogOnAwake : MonoBehaviour
{
    public string[] dialog;
    public UnityEvent eventOnDialogFinish;


    void Start()
    {
        DialogController.instance.ShowDialog(dialog);
    }


    void Update()
    {
        // wait to finish talking
        if(DialogController.instance.IsTalking()) return;

        // invoke event!
        eventOnDialogFinish.Invoke();
        this.enabled = false;
    }
}
