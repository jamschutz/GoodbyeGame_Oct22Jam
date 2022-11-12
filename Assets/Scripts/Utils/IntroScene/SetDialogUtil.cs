using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utils
{
    public class SetDialogUtil : MonoBehaviour
    {
        public string[] dialog;
        public AI.TalkOnInteract talkOnInteract;


        public void SetDialog()
        {
            talkOnInteract.SetDialog(dialog);
        }
    }
}