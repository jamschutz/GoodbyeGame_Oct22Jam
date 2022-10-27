using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DialogController = UI.Controllers.DialogController;

namespace AI
{
    public class TalkOnInteract : MonoBehaviour
    {
        public string[] dialog;

        private bool inTalkingRange;


        private void Start()
        {
            inTalkingRange = false;
        }


        private void Update()
        {
            if(inTalkingRange && Input.GetKeyDown(KeyCode.Space)) {
                // if already showing dialog, ignore
                if(DialogController.instance.IsTalking()) {
                    Debug.Log("waiting for dialog controller to finish current dialog...");
                    return;
                }

                // otherwise, show our dialog!
                DialogController.instance.ShowDialog(dialog);
            }
        }

    
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.tag == "Player") {
                inTalkingRange = true;
            }
        }

    
        private void OnTriggerExit2D(Collider2D collision)
        {
            if(collision.tag == "Player") {
                inTalkingRange = false;
            }
        }
    }
}