using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class autoTalk : MonoBehaviour
{
    public bool inAutoTalkingRange;
    bool firstTime = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && firstTime)
        {
            inAutoTalkingRange = true;
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            inAutoTalkingRange = false;
            firstTime = true;
        }
    }

}
