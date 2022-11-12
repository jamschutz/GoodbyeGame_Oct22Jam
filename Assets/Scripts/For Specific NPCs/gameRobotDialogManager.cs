using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogController = UI.Controllers.DialogController;


public class gameRobotDialogManager : MonoBehaviour
{
    public bool doneIntro;
    public bool finishedD1;
    public AI.TalkOnInteract myDialogManager;
    public string[] dialog01;
    public string[] nothing;
    public string[] d2;
    public GameObject autoTalkRange;
    bool runOnce;

    // Start is called before the first frame update
    void Start()
    {
        myDialogManager = GetComponent<AI.TalkOnInteract>();
    }

    // Update is called once per frame
    void Update()
    {
        doneIntro = DialogController.instance.HasSeenFlag("DoneIntro");
        finishedD1 = DialogController.instance.HasSeenFlag("FinishedD1");
        if (finishedD1)
        {
            myDialogManager.SetDialog(nothing);
            if (!runOnce)
            {
                autoTalkRange.SetActive(true);
                autoTalkRange.GetComponent<autoTalk>().inAutoTalkingRange = false;
                runOnce = true;
            }
           if (autoTalkRange.GetComponent<autoTalk>().inAutoTalkingRange)
            {
                if (DialogController.instance.IsTalking())
                {
                    Debug.Log("waiting for dialog controller to finish current dialog...");
                    return;
                }

                // otherwise, show our dialog!
                DialogController.instance.ShowDialog(d2);
                autoTalkRange.GetComponent<autoTalk>().inAutoTalkingRange = false;
            }

        }
        else if (doneIntro)
        {
            myDialogManager.SetDialog(dialog01);
        }

    }




}
