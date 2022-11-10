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
            ShowVertices();
        }


        private void InitVertices()
        {
            // calculate bounds
            Vector2 start = transform.position;
            Vector2 end = start + new Vector2(width, height);

            Debug.Log($"start: {start.ToString()},   end: {end.ToString()}");

            // create vertices
            vertices = new List<NavMeshVertex>();
            for(float x = start.x; x < end.x; x += distanceBetweenVertices) {
                for(float y = start.y; y < end.y; y += distanceBetweenVertices) {
                    // create vertex at (x, y)
                    var vertex = new NavMeshVertex();
                    vertex.position = new Vector2(x, y);

                    // add to list
                    vertices.Add(vertex);
                }
            }

            Debug.Log($"created {vertices.Count} vertices");
        }


        private void ShowVertices()
        {
            foreach(var v in vertices) {
                var obj = GameObject.CreatePrimitive(PrimitiveType.Cube);
                obj.transform.position = v.position;
                Debug.Log($"created object at {v.position.ToString()}");
            }
        }


        public Vector3[] GetPathBetweenPoints(Vector3 start, Vector3 end)
        {
            var path = new List<Vector3>();

            return path.ToArray();
        }
    }
}

