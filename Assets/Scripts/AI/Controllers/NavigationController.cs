using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI.Controller
{
    public class NavigationController : MonoBehaviour
    {
        public NavMesh navMesh;
        public Transform destination;

        private Vector2[] routeMarkers;





        public Vector2[] GetPathBetweenPoints(Vector2 start, Vector2 end)
        {
            var path = new List<Vector2>();
            List<NavMeshVertex> vertices = navMesh.GetVertices();

            // get vertices nearest start and end
            int s = navMesh.GetClosestVertexIndex(start);
            int e = navMesh.GetClosestVertexIndex(end);

            // set vert IDs
            for(int i = 0; i < vertices.Count; i++) {
                vertices[i].id = i;
            }

            var openSet = new List<NavMeshVertex>();
            var cameFrom = new Dictionary<int, int>();
            var gScore = new Dictionary<int, float>();
            var fScore = new Dictionary<int, float>();

            // add start to open set
            openSet.Add(vertices[s]);
            // init our score maps
            vertices.ForEach(v => gScore.Add(v.id, float.PositiveInfinity));
            vertices.ForEach(v => fScore.Add(v.id, float.PositiveInfinity));
            // score the start
            gScore[vertices[s].id] = 0;
            fScore[vertices[s].id] = GetScore(vertices[s].position, end);

            while (openSet.Count > 0) {
                // rescore our nodes, then sort our set by score
                openSet.ForEach(v => v.searchScore = gScore[v.id] + fScore[v.id]);
                openSet.Sort((v1, v2) => v1.searchScore.CompareTo(v2.searchScore));

                // get the best one
                var current = openSet[0];

                // check if we're at the goal, and if so return
                if(AtGoal(current, end)) {
                    Debug.Log("done!!!!");
                    return new Vector2[]{};
                }

                // pop current node
                openSet.RemoveAt(0);
                foreach(var neighbor in current.neighbors) {
                    var tentativeGScore = gScore[current.id] + navMesh.distanceBetweenVertices;
                    if(tentativeGScore < gScore[neighbor.id]) {
                        // this path to the neighbor is better than the previous one. record it!
                        cameFrom[neighbor.id] = current.id;
                        gScore[neighbor.id] = tentativeGScore;
                        fScore[neighbor.id] = tentativeGScore + GetScore(neighbor.position, end);

                        // add neighbor if not in open set
                        if(!IsVertexInList(neighbor, ref openSet)) {
                            openSet.Add(neighbor);
                        }
                    }
                }
            }

            // no path found -- return empty list
            Debug.LogWarning($"unable to find path between points {start.ToString()} and {end.ToString()}");
            return new Vector2[] {};
        }


        private float GetScore(Vector2 v, Vector2 end)
        {
            return Vector2.Distance(v, end);
        }


        private bool AtGoal(NavMeshVertex v, Vector2 end)
        {
            return Vector2.Distance(v.position, end) < float.Epsilon;
        }


        private bool IsVertexInList(NavMeshVertex vertex, ref List<NavMeshVertex> vertList)
        {
            foreach(var v in vertList) {
                if(v.id == vertex.id) return true;
            }

            return false;
        }
    }
}

