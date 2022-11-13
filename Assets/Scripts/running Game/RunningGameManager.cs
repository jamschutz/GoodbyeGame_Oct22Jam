using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunningGameManager : MonoBehaviour
{
    public float speedUpTime;
    float speedUpTimeTemp;
    public float grassSpeed;
    float grassSpeedTemp;
    public float playerAnimationSpeed;
    public float parentAnimationSpeed;
    float parentZPos;
    float parentXPos;
    public float parentActualSpeed;
    public float parentHorizontalSpeed;

    public GameObject player;
    public GameObject parent;

    //public bool isDrawing;
    public bool isRunning;

    public bool isNormal;
    public bool isTired;
    public bool isCrying;

    bool runOnce;

    // Start is called before the first frame update
    void Start()
    {
        isNormal = true;
        parentZPos = parent.transform.position.z;
        parentXPos = parent.transform.position.x;
        grassSpeedTemp = grassSpeed;
        speedUpTimeTemp = speedUpTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (speedUpTime > 0)
        {
            if (parentZPos < 27) parentZPos += Time.deltaTime * parentActualSpeed;
            if (parentXPos > -8.1f) parentXPos -= Time.deltaTime * parentHorizontalSpeed;
        }
        else
        {
            if (parentZPos > 0) parentZPos -= Time.deltaTime * parentActualSpeed;
            if (parentXPos < -1) parentXPos += Time.deltaTime * parentHorizontalSpeed;
        }

        


        if (!isRunning)//drawing
        {
            
            grassSpeed = 0;
            runOnce = true;
            speedUpTime = -1;
        }
        else//is running
        {
            if (runOnce)
            {
                speedUpTime = speedUpTimeTemp;
                runOnce = false;
            }

            grassSpeed = grassSpeedTemp;
            speedUpTime -= Time.deltaTime;
        }

        parent.transform.position = new Vector3(parentXPos, parent.transform.position.y, parentZPos);

        player.GetComponent<Animator>().speed = playerAnimationSpeed;
        parent.GetComponent<Animator>().speed = parentAnimationSpeed;

        if (isCrying)
        {
            isNormal = false;
            isTired = false;
            if (isRunning)
            {
                player.GetComponent<Animator>().SetBool("isNormalRunning", false);
                player.GetComponent<Animator>().SetBool("isTiredRunning", false);
                player.GetComponent<Animator>().SetBool("isCryingRunning", true);
            }
            else
            {
                player.GetComponent<Animator>().SetBool("isCryingRunning", false);
            }
        }


        if (isTired)
        {
            isNormal = false;
            isCrying = false;
            if (isRunning)
            {
                player.GetComponent<Animator>().SetBool("isNormalRunning", false);
                player.GetComponent<Animator>().SetBool("isTiredRunning", true);
            }
            else
            {
                player.GetComponent<Animator>().SetBool("isTiredRunning", false);
            }
        }


        if (isNormal)
        {
            isTired = false;
            isCrying = false;
            if (isRunning)
            {
                player.GetComponent<Animator>().SetBool("isNormalRunning", true);
            }else
            {
                player.GetComponent<Animator>().SetBool("isNormalRunning", false);
            }
        }

    }
}
