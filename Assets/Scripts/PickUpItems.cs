using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpItems : MonoBehaviour
{
    public KeyCode interactKey;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (Input.GetKeyDown(interactKey))
        {
            if (collision.gameObject.tag == "InteractableItems")
            {
                //collision.gameObject.GetComponent
            }
        }

        
    }
}
