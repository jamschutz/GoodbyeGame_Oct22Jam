using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogController = UI.Controllers.DialogController;


public class gameRobotDialogManager : MonoBehaviour
{
    public bool doneIntro;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        doneIntro = DialogController.instance.HasSeenFlag("DoneIntro");
        if (doneIntro)
        {
            //GetComponent<TalkOnInteract>().enabled = false;
        }

    }
}
