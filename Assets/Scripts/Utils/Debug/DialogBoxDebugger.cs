using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utils.Debug
{
    public class DialogBoxDebugger : MonoBehaviour
    {
        public string[] dialog;

        private int index;
        void Start()
        {
            index = 0;
        }

        void Update()
        {
            if(Input.GetKeyDown(KeyCode.KeypadEnter)) {
                UI.Controllers.DialogController.instance.ShowDialog(dialog);
            }
        }
    }
}

