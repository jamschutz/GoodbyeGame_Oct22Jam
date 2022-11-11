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
        public float minDistanceFromWalls = 2;

        [Header("Editor")]
        public bool showGrid = false;


        private List<NavMeshVertex> vertices;


        // ========================================================== //
        // ===========    Lifecycle Methods                ========== //
        // ========================================================== //

        private void Start()
        {
            InitVertices();
            BuildVertexConnections();
        }


#if UNITY_EDITOR
        private void Update()
        {
            if(!showGrid) return;

            foreach(var vertex in vertices) {
                foreach(var neighbor in vertex.neighbors) {
                    Debug.DrawLine(vertex.position, neighbor.position, Color.red, 0.1f);
                }
            }
        }
#endif



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

                    // check proper distance from walls
                    var walls = Physics2D.OverlapCircleAll(vertex.position, minDistanceFromWalls, Utils.Globals.NavigationLayer);
                    if(walls.Length == 0) {
                        vertices.Add(vertex);
                    }
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
                        // check for things blocking the path...
                        var hit = Physics2D.Raycast(vertex.position, (other.position - vertex.position).normalized, distanceBetweenVertices, Utils.Globals.NavigationLayer);

                        // if nothing in between, register as neighbor
                        if(hit.transform == null) {
                            vertex.neighbors.Add(other);
                        }
                    }
                }
            }
        }


        public Vector3[] GetPathBetweenPoints(Vector3 start, Vector3 end)
        {
            var path = new List<Vector3>();

            // get vertices nearest start and end
            int s = GetClosestVertexIndex(start);
            int e = GetClosestVertexIndex(end);
            Vector2 startingVertex = vertices[s].position;
            Vector2 endingVertex = vertices[e].position;

            var availableVertices = vertices;
            availableVertices.RemoveAt(s);
            availableVertices.RemoveAt(e);

            


            return path.ToArray();
        }


        public List<NavMeshVertex> GetVertices()
        {
            return vertices;
        }


        public int GetClosestVertexIndex(Vector2 pos)
        {
            float bestDistance = 1000000;
            int vertIndex = 0;
            for(int i = 0; i < vertices.Count; i++) {
                if(Vector2.Distance(pos, vertices[i].position) < bestDistance) {
                    vertIndex = i;
                    bestDistance = Vector2.Distance(pos, vertices[i].position);
                }
            }

            return vertIndex;
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
    }
}

