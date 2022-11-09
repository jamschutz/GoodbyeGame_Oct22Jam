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

        // special strings
        public const string PLAYER_NAME_SPECIAL = "{PLAYER_NAME}";
        public const string NEW_LINE_SPECIAL = "{NEW_LINE}";
        public const string CHOICE_DECLARATION = "{CHOICE}";
        public const string CHOICE_OPTIONS_DECLARATION = "{CHOICES: ";
        public const string DECISIONS_DECLARATION = "{DECISIONS: ";

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
            if(Input.GetKeyDown(KeyCode.Space)) {
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
            InjectVariables(ref dialog);
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
                if(IsChoice(line)) {
                    // remove choice declaration
                    correctedLine = line.Replace(CHOICE_DECLARATION, "");

                    // find the {CHOICES: 1 | 2} index bounds
                    int choiceStart = correctedLine.IndexOf(CHOICE_OPTIONS_DECLARATION) + CHOICE_OPTIONS_DECLARATION.Length;
                    int choiceEnd = correctedLine.IndexOf("}",choiceStart);

                    Debug.Log($"choicestart: {choiceStart}....first part: {correctedLine.Substring(0,choiceStart)}; second part: {correctedLine.Substring(choiceStart)}");
                    
                    // grab just the "1 | 2" from the above
                    string rawChoices = correctedLine.Substring(choiceStart, choiceEnd - choiceStart);
                    // separate choices into strings
                    string[] choices = rawChoices.Split(" | ");

                    // combine everything
                    correctedLine = correctedLine.Substring(0, choiceStart - CHOICE_OPTIONS_DECLARATION.Length) + "\n";
                    // show options
                    for(int i = 0; i < choices.Length; i++) {
                        correctedLine += $"\n{i + 1}: {choices[i]}";
                    }
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


        private void InjectVariables(ref string[] dialog)
        {
            for(int i = 0; i < dialog.Length; i++) {
                dialog[i] = dialog[i].Replace(PLAYER_NAME_SPECIAL, GameManager.PlayerName);
                dialog[i] = dialog[i].Replace(NEW_LINE_SPECIAL, "\n");
            }
        }


        private bool IsChoice(string dialog)
        {
            return dialog.Substring(0,CHOICE_DECLARATION.Length) == CHOICE_DECLARATION;
        }
    }
}
