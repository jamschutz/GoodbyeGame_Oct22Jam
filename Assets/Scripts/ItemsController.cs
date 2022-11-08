using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogController = UI.Controllers.DialogController;

public class ItemsController : MonoBehaviour
{
    GameManager myGameManager;
    public bool interacted;

    [Header("Types of Interaction")]
    public bool pickUp;
    public bool dialogBox;

    [Header("Dialog")]
    public string[] dialogContents;

    // Start is called before the first frame update
    void Start()
    {
        myGameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (interacted)
        {
            if (pickUp)
            {
                pickedUp();
            }

            if (dialogBox && !DialogController.instance.IsTalking())
            {
                showDialog();
            }
        }
    }

    public void showDialog()
    {
        DialogController.instance.ShowDialog(dialogContents);
        interacted = false;
    }

    public void pickedUp()
    {
        myGameManager.Disc_Captain++;
        gameObject.SetActive(false);
        interacted = false;
    }

   

}
