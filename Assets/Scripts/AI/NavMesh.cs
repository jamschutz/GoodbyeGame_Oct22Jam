using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class NavMesh : MonoBehaviour
    {
        [Header("Vertices")]
        public float distanceBetweenVertices = 5;
        public float height;
        public float width;

        [Header("Editor")]
        public float gizmoRadius = 1;


        private List<NavMeshVertex> vertices;

        private void Start()
        {
            InitVertices();
        }


        private void InitVertices()
        {
            // calculate bounds
            Vector2 start = transform.position;
            Vector2 end = start + new Vector2(width, height);

            // create vertices
            vertices = new List<NavMeshVertex>();
            for(float x = start.x; x < end.x; x += distanceBetweenVertices) {
                for(float y = start.y; y < end.y; y += distanceBetweenVertices) {
                    var vertex = new NavMeshVertex();
                    vertex.position = new Vector2(x, y);
                }
            }
        }


        private void ShowVertices()
        {
            foreach(var v in vertices) {
                // GameObject.Instantiate(PrimitiveType.Cube, v.position, Quaternion.identity);
            }
        }


        public Vector3[] GetPathBetweenPoints(Vector3 start, Vector3 end)
        {
            var path = new List<Vector3>();

            return path.ToArray();
        }
    }
}

