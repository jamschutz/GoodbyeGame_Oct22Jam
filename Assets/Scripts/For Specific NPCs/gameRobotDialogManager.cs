using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogController = UI.Controllers.DialogController;


public class gameRobotDialogManager : MonoBehaviour
{
    public bool doneIntro;
    public bool finishedD1;
    public bool finishedD2;
    public bool finishedD3;
    public bool finishedD4;
    public bool finishedD5;
    public bool finishedD6;
    public bool finishedD7;
    public bool finishedD8;
    public bool finishedD9;
    public bool finishedD10;
    public AI.TalkOnInteract myDialogManager;
    public string[] dialog01;
    public string[] nothing;
    public string[] d2;
    public string[] d3;
    public string[] d4;
    public string[] d5;
    public string[] d6;
    public string[] d7;
    public string[] d8;
    public string[] d9;
    public string[] d10;
    public string[] d11;
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
        finishedD2 = DialogController.instance.HasSeenFlag("FinishedD2");
        finishedD3 = DialogController.instance.HasSeenFlag("FinishedD3");
        finishedD4 = DialogController.instance.HasSeenFlag("FinishedD4");
        finishedD5 = DialogController.instance.HasSeenFlag("FinishedD5");
        finishedD6 = DialogController.instance.HasSeenFlag("FinishedD6");
        finishedD7 = DialogController.instance.HasSeenFlag("FinishedD7");
        finishedD8 = DialogController.instance.HasSeenFlag("FinishedD8");
        finishedD9 = DialogController.instance.HasSeenFlag("FinishedD9");
        finishedD10 = DialogController.instance.HasSeenFlag("FinishedD10");

        if (finishedD10)
        {
            myDialogManager.SetDialog(d11);
        }
        else if (finishedD9)
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
                DialogController.instance.ShowDialog(d10);
                autoTalkRange.GetComponent<autoTalk>().inAutoTalkingRange = false;
            }

        }
        else if (finishedD8)
        {
            myDialogManager.SetDialog(d9);
            runOnce = false;
        }
        else if (finishedD7)
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
                DialogController.instance.ShowDialog(d8);
                autoTalkRange.GetComponent<autoTalk>().inAutoTalkingRange = false;
            }
        }
        else if (finishedD6)
        {
            myDialogManager.SetDialog(d7);
            runOnce = false;
        }
        else if (finishedD5)
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
                DialogController.instance.ShowDialog(d6);
                autoTalkRange.GetComponent<autoTalk>().inAutoTalkingRange = false;
            }
        }
        else if (finishedD4)
        {
            myDialogManager.SetDialog(d5);
            runOnce = false;
        }
        else  if (finishedD3)
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
                DialogController.instance.ShowDialog(d4);
                autoTalkRange.GetComponent<autoTalk>().inAutoTalkingRange = false;
            }

        }
        else if (finishedD2)
        {
            myDialogManager.SetDialog(d3);
            runOnce = false;
        }
        else if (finishedD1)
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
