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


        private void ShowVertices()
        {
            foreach(var v in vertices) {
                var obj = GameObject.CreatePrimitive(PrimitiveType.Cube);
                obj.transform.position = v.position;
            }
        }


        public Vector3[] GetPathBetweenPoints(Vector3 start, Vector3 end)
        {
            var path = new List<Vector3>();

            return path.ToArray();
        }


        private Vector2 GetStart()
        {
            // grab collider
            var collider = GetComponent<BoxCollider2D>();
            var center = new Vector2(transform.position.x, transform.position.y) + collider.offset;

            // get bounds
            float left  = center.x - (collider.size.x * 0.5f);
            float right = center.x + (collider.size.x * 0.5f);
            float top    = center.y - (collider.size.y * 0.5f);
            float bottom = center.y + (collider.size.y * 0.5f);

            return new Vector2(left, top);
        }


        private Vector2 GetEnd()
        {
            // grab collider
            var collider = GetComponent<BoxCollider2D>();
            var center = new Vector2(transform.position.x, transform.position.y) + collider.offset;

            // get bounds
            float left  = center.x - (collider.size.x * 0.5f);
            float right = center.x + (collider.size.x * 0.5f);
            float top    = center.y - (collider.size.y * 0.5f);
            float bottom = center.y + (collider.size.y * 0.5f);

            return new Vector2(right, bottom);
        }
    }
}

