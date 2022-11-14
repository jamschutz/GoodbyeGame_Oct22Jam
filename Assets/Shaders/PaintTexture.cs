using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PaintTexture : MonoBehaviour
{
    [Header("Material Reference")]
    public Material paintableMaterial;

    [Header("Drawing")]
    public int pixelRadius;
    public Color paintColor;
    public float mouseStep;

    [Header("Paper")]
    public Texture2D[] paperTextures;


    private Vector2 lastMouseInput;
    private List<Texture2D> dogPaintings;
    private Image image;
    private RectTransform rectTransform;
    private Canvas canvas;
    private Texture2D activePaperTexture;


    private void Start()
    {
        image = GetComponent<Image>();
        dogPaintings = new List<Texture2D>();
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
        lastMouseInput = Vector2.negativeInfinity;

        CreateNewPainting();
    }


    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)) {
            SaveAndClearImage();
        }
        if(Input.GetMouseButton(0)) {
            // no input last frame
            if(lastMouseInput.x < 0) {
                var mousePosition = Input.mousePosition;
                PaintFromMousePosition(mousePosition);
            }
            // draw between last input
            else {
                Vector2 mousePosition = Input.mousePosition;
                var direction = (mousePosition - lastMouseInput).normalized;

                for(Vector2 pos = lastMouseInput; Vector2.Distance(pos, mousePosition) > mouseStep; pos += direction * mouseStep) {
                    PaintFromMousePosition(pos);
                }
                PaintFromMousePosition(mousePosition);
            }
            lastMouseInput = Input.mousePosition;
            
        }
        else {
            lastMouseInput = Vector2.negativeInfinity;
        }
    }


    public Texture2D[] GetPaintings()
    {
        // remove last painting
        if(dogPaintings.Count > 0) {
            dogPaintings.RemoveAt(dogPaintings.Count - 1);
        }
        return dogPaintings.ToArray();
    }


    public void SaveAndClearImage()
    {
        // Encode texture into PNG
		byte[] bytes = dogPaintings[dogPaintings.Count - 1].EncodeToPNG();

		// For testing purposes, also write to a file in the project folder
		System.IO.File.WriteAllBytes(Application.dataPath + "TestDrawing.png", bytes);
        CreateNewPainting();
    }


    private void CreateNewPainting()
    {
        var painting = new Texture2D(500, 500);
        activePaperTexture = paperTextures[Random.Range(0,paperTextures.Length)];

        CopyTextures(ref activePaperTexture, ref painting);
        dogPaintings.Add(painting);
        
        paintableMaterial.SetTexture("_MainTex", painting);
        image.SetMaterialDirty();
    }


    private void PaintFromMousePosition(Vector2 mousePosition)
    {
        var paintingCenter = rectTransform.position;
        var paintingDimensions =  new Vector2(rectTransform.rect.width * canvas.scaleFactor, rectTransform.rect.height * canvas.scaleFactor);
        // Debug.Log($"center: {paintingCenter}, dimensions: {paintingDimensions}, canvas scale: {GetComponentInParent<Canvas>().scaleFactor}");
        var screenCenter = new Vector2(Screen.width * 0.5f, Screen.height * 0.5f);

        var xDistance = Mathf.Abs(mousePosition.x - screenCenter.x);
        var yDistance = Mathf.Abs(mousePosition.y - screenCenter.y);

        var clickedOnImage = xDistance < (paintingDimensions.x * 0.5f) && yDistance < (paintingDimensions.y * 0.5f);
        
        if(clickedOnImage) {
            // scale to [0,1]
            var x = (float)(mousePosition.x - (screenCenter.x - paintingDimensions.x * 0.5)) / paintingDimensions.x;
            var y = (float)(mousePosition.y - (screenCenter.y - paintingDimensions.y * 0.5)) / paintingDimensions.y;

            PaintPixelCoordinate(new Vector2(x, y));
        }
    }



    private void PaintPixelCoordinate(Vector2 coord)
    {
        var texture = dogPaintings[dogPaintings.Count - 1];

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

                    //sourceColour*sourceAlpha + destinationColour*oneMinusSourceAlpha

                    // blend with source color
                    var sourceColor = texture.GetPixel(x, y);
                    var alphaColor = (sourceColor * (1 - paintColor.a)) + (paintColor * (paintColor.a));

                    texture.SetPixel(x, y, (activePaperTexture.GetPixel(x, y) * (1 - paintColor.a)) + (paintColor * (paintColor.a)));
                    texture.SetPixel(x, ySym, (activePaperTexture.GetPixel(x, ySym) * (1 - paintColor.a)) + (paintColor * (paintColor.a)));
                    texture.SetPixel(xSym, y, (activePaperTexture.GetPixel(xSym, y) * (1 - paintColor.a)) + (paintColor * (paintColor.a)));
                    texture.SetPixel(xSym, ySym, (activePaperTexture.GetPixel(xSym, ySym) * (1 - paintColor.a)) + (paintColor * (paintColor.a)));
                }
            }
        }
        texture.Apply();
    }


    private void CopyTextures(ref Texture2D src, ref Texture2D dest)
    {
        for (int y = 0; y < src.height; y++)
        {
            for (int x = 0; x < src.width; x++)
            {
                // read src pixel color
                Color col = src.GetPixel(x, y);
                
                // get nearest pixel in dest
                int nearestX = Mathf.RoundToInt(((float)x / (float)src.width) * dest.width);
                int nearestY = Mathf.RoundToInt(((float)y / (float)src.height) * dest.height);

                // and set to src color
                dest.SetPixel(nearestX, nearestY, col);
            }
        }

        dest.Apply();
    }
}
