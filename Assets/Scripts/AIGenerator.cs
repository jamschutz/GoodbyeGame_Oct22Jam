using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIGenerator : MonoBehaviour
{
    public GameObject[] AIs;
    public Sprite[] hatSprites;
    public GameObject surface;
    public GameManager myManager;
    public bool interacted;
    int hatIndex;
    bool generatorIsWorking;
    GameObject hat;
    // Start is called before the first frame update
    void Start()
    {
        hatIndex = 0;
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
                GameObject captain = Instantiate(AIs[0]);
                captain.transform.position = surface.transform.position;
                hat = captain.transform.Find("Hat").gameObject;
                hat.GetComponent<SpriteRenderer>().sprite = hatSprites[hatIndex];
                generatorIsWorking = true;
                myManager.Disc_Captain --;
            }
            if (generatorIsWorking)
            {
                Debug.Log(hatIndex);
                if (Input.GetKeyDown(KeyCode.RightArrow))
                {
                    if (hatIndex == hatSprites.Length-1)
                    {
                        hatIndex = 0;
                    }
                    else
                    {
                        hatIndex++;
                    }
                    

                }
                if (Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    if (hatIndex == 0)
                    {
                        hatIndex = hatSprites.Length - 1;
                    }
                    else
                    {
                        hatIndex--;
                    }

                }
                hat.GetComponent<SpriteRenderer>().sprite = hatSprites[hatIndex];


            }



            if (Input.GetKeyDown(KeyCode.Space))
            {
                surface.SetActive(false);
                interacted = false;
                generatorIsWorking = false;
            }

        }
        
    }
}
