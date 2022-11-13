using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using  UnityEngine.EventSystems;

public class PaintTexture : MonoBehaviour
{
    public Material paintableMaterial;
    public int pixelRadius;
    public Color paintColor;
    [Range(0,0.1f)]
    public float mouseStep;


    private Texture2D texture;
    private Vector2 lastMouseInput;


    private void Start()
    {
        texture = new Texture2D(500, 500);
        paintableMaterial.mainTexture = texture;

        for (int y = 0; y < texture.height; y++)
        {
            for (int x = 0; x < texture.width; x++)
            {
                Color color = ((x & y) != 0 ? Color.white : Color.gray);
                texture.SetPixel(x, y, Color.white);
            }
        }
        texture.Apply();

        lastMouseInput = Vector2.negativeInfinity;
    }


    private void Update()
    {
        if(Input.GetMouseButton(0)) {
            var mousePosition = Input.mousePosition;
            PaintFromMousePosition(mousePosition);
        }
        else {
            lastMouseInput = Vector2.negativeInfinity;
        }
    }


    private void PaintFromMousePosition(Vector2 mousePosition)
    {
        var screenCenter = new Vector2(Screen.width * 0.5f, Screen.height * 0.5f);

        var xDistance = Mathf.Abs(mousePosition.x - screenCenter.x);
        var yDistance = Mathf.Abs(mousePosition.y - screenCenter.y);

        var clickedOnImage = xDistance < 250 && yDistance < 250;
        
        if(clickedOnImage) {
            // scale to [0,1]
            var x = (mousePosition.x - (screenCenter.x - 250)) / 500;
            var y = (mousePosition.y - (screenCenter.y - 250)) / 500;

            PaintPixelCoordinate(new Vector2(x, y));
        }
    }



    private void PaintPixelCoordinate(Vector2 coord)
    {
        int xCenter = (int)(coord.x * texture.width);
        int yCenter = (int)(coord.y * texture.height);

        

        for (int x = xCenter - pixelRadius ; x <= xCenter; x++)
        {
            for (int y = yCenter - pixelRadius ; y <= yCenter; y++)
            {
                // we don't have to take the square root, it's slow
                if ((x - xCenter)*(x - xCenter) + (y - yCenter)*(y - yCenter) <= pixelRadius * pixelRadius) 
                {
                    int xSym = xCenter - (x - xCenter);
                    int ySym = yCenter - (y - yCenter);
                    // (x, y), (x, ySym), (xSym , y), (xSym, ySym) are in the circle

                    texture.SetPixel(x, y, paintColor);
                    texture.SetPixel(x, ySym, paintColor);
                    texture.SetPixel(xSym, y, paintColor);
                    texture.SetPixel(xSym, ySym, paintColor);
                }
            }
        }
        texture.Apply();
    }

    
     private void DebugPoint(Vector2 mousePosition)
     {
         Vector2 localCursor;
         if (!RectTransformUtility.ScreenPointToLocalPointInRectangle(GetComponent<RectTransform>(), mousePosition, Camera.main, out localCursor))
             return;
 
         Debug.Log("LocalCursor:" + localCursor.ToString());
     }
}
