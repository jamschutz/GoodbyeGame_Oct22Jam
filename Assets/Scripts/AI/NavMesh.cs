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


        // ========================================================== //
        // ===========    Lifecycle Methods                ========== //
        // ========================================================== //

        private void Start()
        {
            InitVertices();
            BuildVertexConnections();
            // ShowVertices();
        }


        private void Update()
        {
            foreach(var vertex in vertices) {
                foreach(var neighbor in vertex.neighbors) {
                    Debug.DrawLine(vertex.position, neighbor.position, Color.red, 1);
                }
            }
        }




        // ========================================================== //
        // ===========    Main Methods                     ========== //
        // ========================================================== //


        private void InitVertices()
        {
            // calculate bounds
            Vector2 start = GetStart();
            Vector2 end = GetEnd();

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
        }


        private void BuildVertexConnections()
        {
            // for each vertex...
            foreach(var vertex in vertices) {
                // look at every other vertex
                foreach(var other in vertices) {
                    // get distance
                    float distance = Vector2.Distance(vertex.position, other.position);

                    // if this is us, ignore...
                    if(distance < float.Epsilon) continue;

                    // otherwise, check if it's within max distance of us
                    if(distance < distanceBetweenVertices + 0.2f) {
                        // and if so, register neighbor
                        vertex.neighbors.Add(other);
                    }
                }
            }
        }


        public Vector3[] GetPathBetweenPoints(Vector3 start, Vector3 end)
        {
            var path = new List<Vector3>();

            return path.ToArray();
        }




        // ========================================================== //
        // ===========    Helper Methods                   ========== //
        // ========================================================== //


        private Vector2 GetStart()
        {
            // grab collider
            var collider = GetComponent<BoxCollider2D>();
            var center = new Vector2(transform.position.x, transform.position.y) + collider.offset;

            // get bounds
            float left = center.x - (collider.size.x * 0.5f);
            float top = center.y - (collider.size.y * 0.5f);

            return new Vector2(left, top);
        }


        private Vector2 GetEnd()
        {
            // grab collider
            var collider = GetComponent<BoxCollider2D>();
            var center = new Vector2(transform.position.x, transform.position.y) + collider.offset;

            // get bounds
            float right = center.x + (collider.size.x * 0.5f);
            float bottom = center.y + (collider.size.y * 0.5f);

            return new Vector2(right, bottom);
        }


        private void ShowVertices()
        {
            foreach(var v in vertices) {
                var obj = GameObject.CreatePrimitive(PrimitiveType.Cube);
                obj.transform.position = v.position;
            }
        }
    }
}

