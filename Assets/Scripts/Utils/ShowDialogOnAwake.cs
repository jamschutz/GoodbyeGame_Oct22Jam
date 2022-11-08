using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DialogController = UI.Controllers.DialogController;

public class ShowDialogOnAwake : MonoBehaviour
{
    public string[] dialog;


    void Start()
    {
        DialogController.instance.ShowDialog(dialog);
    }
}
