using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utils.IntroScene
{
    public class IntroController : MonoBehaviour
    {
        public GameObject[] titleScreens;


        private int currentScreen;

        private void Start()
        {
            currentScreen = 0;
        }


        public void ShowNextScreen()
        {
            titleScreens[currentScreen].SetActive(false);
            titleScreens[++currentScreen].SetActive(true);
        }
    }
}

