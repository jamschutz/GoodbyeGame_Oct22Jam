using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Utils.IntroScene
{
    public class IntroController : MonoBehaviour
    {
        public GameObject[] titleScreens;
        public UnityEvent eventOnComplete;


        private int currentScreen;

        private void Start()
        {
            currentScreen = 0;
        }


        public void ShowNextScreen()
        {
            titleScreens[currentScreen++].SetActive(false);
            if(currentScreen < titleScreens.Length) {
                titleScreens[currentScreen].SetActive(true);
            }
            else {
                eventOnComplete.Invoke();
            }
        }
    }
}

