using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Utils.IntroScene
{
    public class SpacebarToProceed : MonoBehaviour
    {
        public IntroController introController;


        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.Space)) {
                introController.ShowNextScreen();
            }
        }
    }
}