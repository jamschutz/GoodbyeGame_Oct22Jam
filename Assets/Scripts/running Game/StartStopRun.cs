using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartStopRun : MonoBehaviour
{
    public RunningGameManager gameManager;

    private const int LEFT_MOUSE = 0;
    private const int RIGHT_MOUSE = 1;


    private void Update()
    {
        if(Input.GetMouseButtonDown(LEFT_MOUSE)) {
            gameManager.isRunning = false;
        }

        if(Input.GetMouseButton(RIGHT_MOUSE)) {
            gameManager.isRunning = true;
        }
    }
}
