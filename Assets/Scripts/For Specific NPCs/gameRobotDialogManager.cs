using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogController = UI.Controllers.DialogController;


public class gameRobotDialogManager : MonoBehaviour
{
    public bool doneIntro;
    public AI.TalkOnInteract myDialogManager;
    public string[] dialog01;

    // Start is called before the first frame update
    void Start()
    {
        myDialogManager = GetComponent<AI.TalkOnInteract>();
    }

    // Update is called once per frame
    void Update()
    {
        doneIntro = DialogController.instance.HasSeenFlag("DoneIntro");
        if (doneIntro)
        {
            myDialogManager.SetDialog(dialog01);
        }

    }
}
