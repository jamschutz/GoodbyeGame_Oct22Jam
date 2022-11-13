using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EndingPaperController : MonoBehaviour
{
    public GameObject paperPrefab;
    public float timeBetweenDrawings = 3;
    public UnityEvent eventBeforeShowPaper;
    public PaintTexture paintScript;


    private Texture2D[] drawings;


    private void Start()
    {

    }


    private void Update()
    {
        if(Input.GetKeyDown("1")) {
            ShowDrawings(paintScript.GetPaintings());
        }
    }



    public void ShowDrawings(Texture2D[] _drawings)
    {
        drawings = _drawings;
        eventBeforeShowPaper.Invoke();
        Debug.Log($"got {drawings.Length} drawings");
        StartCoroutine("DrawingShower");
    }


    private IEnumerator DrawingShower()
    {
        GameObject lastPaperObject = null;
        foreach(var drawing in drawings) {
            if(lastPaperObject != null) {
                // turn off last paper object
                lastPaperObject.SetActive(false);
            }

            // create new drawing
            lastPaperObject = GameObject.Instantiate(paperPrefab, Vector3.zero, Quaternion.identity);

            // set drawing texture to the drawing
            lastPaperObject.GetComponentInChildren<MeshRenderer>().material.mainTexture = drawing;

            // pause
            yield return new WaitForSeconds(timeBetweenDrawings);
        }
    }
}

