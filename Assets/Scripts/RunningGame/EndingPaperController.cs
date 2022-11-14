using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

using System.Linq;

public class EndingPaperController : MonoBehaviour
{
    public GameObject paperPrefab;
    public float timeBetweenDrawings = 3;
    public UnityEvent eventBeforeShowPaper;
    public PaintTexture paintScript;

    public float minTimeBetweenDrawings = 0.2f;
    public AnimationCurve drawingCurve;
    public int numDrawingsBeforeMin = 10;
    public float minSpeed = 1;
    public float maxSpeed = 2;
    public float heightBetweenDrawings = 0.045f;
    public UnityEvent eventOnEnd;


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
        int drawingsShown = 0;

        // instantiate drawing objects
        var paperObjects = new List<GameObject>();
        float paperHeight = heightBetweenDrawings * drawings.Length;
        foreach(var drawing in drawings) {
            // create new drawing
            var obj = GameObject.Instantiate(paperPrefab, Vector3.up * paperHeight, Quaternion.identity);
            // set drawing texture to the drawing
            obj.GetComponentInChildren<MeshRenderer>().material.mainTexture = drawing;

            paperObjects.Add(obj);
            paperHeight -= heightBetweenDrawings;
        }
        foreach(var obj in paperObjects) {
            // pause
            float lerp = drawingCurve.Evaluate((float)drawingsShown++ / (float)numDrawingsBeforeMin);
            float waitTime = Mathf.Lerp(timeBetweenDrawings, minTimeBetweenDrawings, lerp);
            float animSpeed = Mathf.Lerp(minSpeed, maxSpeed, lerp);
            // timeBetweenDrawings *= 0.6f;
            yield return new WaitForSeconds(waitTime);
            obj.GetComponent<Animator>().SetTrigger("FlipPaper");
        }

        eventOnEnd.Invoke();
    }
}

