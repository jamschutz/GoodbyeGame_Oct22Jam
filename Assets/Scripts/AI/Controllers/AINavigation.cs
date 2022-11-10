using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Linq;


namespace AI.Controller
{
    public class AINavigation : MonoBehaviour
    {
        [Header("Navigation")]
        public LayerMask navigationLayer;
        public float raycastDistance;

        private SpriteRenderer renderer;


        private void Start()
        {
            var renderer = GetComponent<SpriteRenderer>();
        }


        private bool CanGoDown(ref Vector3[] spriteCorners)
        {
            var bottomLeft = spriteCorners[3];
            var bottomRight = spriteCorners[2];

            // Cast a ray straight down.
            var leftHit = Physics2D.Raycast(bottomLeft, -Vector2.up, raycastDistance);
            var rightHit = Physics2D.Raycast(bottomRight, -Vector2.up, raycastDistance);

            // return that we hit nothing
            return leftHit.collider == null && rightHit.collider == null;
        }


        // copied from: https://answers.unity.com/questions/1451688/how-do-i-get-the-positions-of-the-corners-of-a-spr.html
        private Vector3[] GetSpriteCorners()
        {
            Vector3 topRight = renderer.transform.TransformPoint(renderer.sprite.bounds.max);
            Vector3 topLeft = renderer.transform.TransformPoint(new Vector3(renderer.sprite.bounds.max.x, renderer.sprite.bounds.min.y, 0));
            Vector3 botLeft = renderer.transform.TransformPoint(renderer.sprite.bounds.min);
            Vector3 botRight = renderer.transform.TransformPoint(new Vector3(renderer.sprite.bounds.min.x, renderer.sprite.bounds.max.y, 0));
            return new Vector3[] { topRight, topLeft, botLeft, botRight };
        }
    }
}