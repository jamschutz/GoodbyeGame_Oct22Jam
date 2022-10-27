using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


namespace UI.Controllers
{
    public class DialogController : MonoBehaviour
    {
        [Header("Typing Variables")]
        [Range(300, 3000)]
        public int charsPerMinute;

        // singleton instance
        public static DialogController instance;


        // private TMP variables
        private TMP_Text text;
        private string[] currentDialog;
        private float waitBetweenChars;

        // game object references
        private GameObject textObj;
        private GameObject backgroundObj;

        // input helpers
        private bool gotInput;

        private void Awake()
        {
            // ensure singleton instance 
            if(DialogController.instance != null && DialogController.instance != this) {
                Destroy(this.gameObject);
            }
            else {
                DialogController.instance = this;
            }
        }


        private void Start()
        {
            // get components
            text = GetComponentInChildren<TMP_Text>();
            textObj = text.gameObject;
            backgroundObj = GetComponentInChildren<UnityEngine.UI.Image>().gameObject;

            // init vars
            waitBetweenChars = 60.0f / (float)charsPerMinute;
            text.text = "";

            // hide box
            HideDialogBox();
        }


        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.Space)) {
                gotInput = true;
            }
        }


        public void ShowDialog(string[] dialog)
        {
            // set to dialog, and add an empty string to the end (to close the dialog out)
            currentDialog = dialog;
            StopAllCoroutines();

            ShowDialogBox();
            StartCoroutine("TypeText");
        }


        private IEnumerator TypeText()
        {
            // reset previous input registers
            gotInput = false;

            // show dialog
            foreach(var line in currentDialog) {
                // reset text
                text.text = "";

                // type out each letter
                foreach(var c in line) {
                    text.text += c;

                    // if player presses space this frame, just show the rest of the line
                    if(gotInput) {
                        text.text = line;

                        // wipe input flag so it doesn't get reused
                        gotInput = false;
                        break;
                    }
                    yield return new WaitForSeconds(waitBetweenChars);
                }

                // wait for input
                while(true) {
                    if(gotInput) {
                        gotInput = false;
                        break;
                    }
                    yield return null;
                }
            }

            text.text = "";
            HideDialogBox();
        }


        private void ShowDialogBox()
        {
            textObj.SetActive(true);
            backgroundObj.SetActive(true);
        }


        private void HideDialogBox()
        {
            textObj.SetActive(false);
            backgroundObj.SetActive(false);
        }
    }
}
