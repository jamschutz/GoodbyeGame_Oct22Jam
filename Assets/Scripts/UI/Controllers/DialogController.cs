using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Utils;


namespace UI.Controllers
{
    public class DialogController : MonoBehaviour
    {
        [Header("Typing Variables")]
        [Range(300, 3000)]
        public int charsPerMinute;

        [Header("UI")]
        public float yPos;

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
        private bool isTalking;
        private bool isMakingChoice;
        private int lastChoice;

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
            transform.localPosition = new Vector3(0, yPos, 0);
            isTalking = false;
            isMakingChoice = false;

            // hide box
            HideDialogBox();
        }


        private void Update()
        {
            // check for number keys
            if(isMakingChoice) {
                // default got input to true (we'll correct in else statement)
                gotInput = true;

                // check number keys registered...
                if(Input.GetKeyDown("1"))      lastChoice = 1;
                else if(Input.GetKeyDown("2")) lastChoice = 2;
                else if(Input.GetKeyDown("3")) lastChoice = 3;
                else if(Input.GetKeyDown("4")) lastChoice = 4;
                else if(Input.GetKeyDown("5")) lastChoice = 5;
                else if(Input.GetKeyDown("6")) lastChoice = 6;
                else if(Input.GetKeyDown("7")) lastChoice = 7;
                else if(Input.GetKeyDown("8")) lastChoice = 8;
                else if(Input.GetKeyDown("9")) lastChoice = 9;

                // register no input this frame
                else gotInput = false;
            }
            // check for space
            else if(Input.GetKeyDown(KeyCode.Space)) {
                gotInput = true;
            }
            
        }


        public void ShowDialog(string dialog)
        {
            ShowDialog(new string[] { dialog });
        }


        public void ShowDialog(string[] dialog)
        {
            // replace special strings
            DialogUtils.InjectVariables(ref dialog);
            // set to dialog, and add an empty string to the end (to close the dialog out)
            currentDialog = dialog;
            StopAllCoroutines();

            ShowDialogBox();
            StartCoroutine("TypeText");
        }


        public bool IsTalking()
        {
            return isTalking;
        }


        private IEnumerator TypeText()
        {
            // reset previous input registers
            gotInput = false;
            isTalking = true;

            // show dialog
            foreach(var line in currentDialog) {
                // reset text
                text.text = "";
                string correctedLine = line;

                // check if it's a choice...
                isMakingChoice = DialogUtils.IsChoice(line);
                if(isMakingChoice) {
                    correctedLine = DialogUtils.GetCorrectedChoiceLine(line);
                }
                // check if decisions list
                else if(DialogUtils.IsDecisionList(line)) {
                    correctedLine = DialogUtils.GetChoice(lastChoice, line);
                }

                // type out each letter
                foreach(var c in correctedLine) {
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
            isTalking = false;
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
