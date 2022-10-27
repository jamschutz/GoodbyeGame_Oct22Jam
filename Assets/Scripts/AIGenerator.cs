using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIGenerator : MonoBehaviour
{
    public GameObject[] AIs;
    public GameObject[] hats;
    public GameObject surface;
    public GameManager myManager;
    public bool interacted;

    // Start is called before the first frame update
    void Start()
    {
        myManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (interacted)
        {
            if (myManager.Disc_Captain > 0)
            {
                surface.SetActive(true);
                Instantiate(AIs[0]);
                AIs[0].transform.position = surface.transform.position;
                myManager.Disc_Captain --;
            }



            if (Input.GetKeyDown(KeyCode.Space))
            {
                surface.SetActive(false);
                interacted = false;
            }

        }
        
    }
}
