using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpItems : MonoBehaviour
{
    public KeyCode interactKey;
    public bool pressedInteractKey;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(interactKey))
        {
            pressedInteractKey = true;
        }

        if (Input.GetKeyUp(interactKey))
        {
            pressedInteractKey = false;
        }


    }

   
    
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "InteractableItems" && pressedInteractKey)
        {
            //Debug.Log("123");
            collision.gameObject.GetComponent<ItemsController>().pickedUp();
        }
    }
}
