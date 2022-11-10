using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Linq;

namespace AI
{
    public class NavMeshVertex : MonoBehaviour
    {
        public NavMeshVertex[] neighborVertices { get; set; }

        public const float MAX_NEIGHBOR_DISTANCE = 5;

        private void Awake()
        {
        #if UNITY_EDITOR
            // assert we're on the right layer
            if((int)gameObject.layer != Utils.Globals.NavigationLayerId) {
                Debug.LogWarning($"Navigation vertex {gameObject.name} is not on the Navigation layer. Navigation may not work as expected");
            }
        #endif

            // init list
            neighborVertices = GetNeighboringVertices();
            Debug.Log($"{gameObject.name} has {neighborVertices.Length} neighboring vertices");
        }


        private void Start()
        {
            // delete helper circle collider and rigidbodies
            var rb = GetComponent<Rigidbody2D>();
            var collider = GetComponent<CircleCollider2D>();
            Destroy(collider);
            Destroy(rb);
        }



        private NavMeshVertex[] GetNeighboringVertices()
        {
            // init list
            var vertices = new List<NavMeshVertex>();

            // find neighbors in range
            var objectsInRange = Physics2D.OverlapCircleAll(transform.position, MAX_NEIGHBOR_DISTANCE, Utils.Globals.NavigationLayer);
            
            // grab all the navmesh vertices
            foreach(var obj in objectsInRange) {
                var navmeshVertex = obj.GetComponent<NavMeshVertex>();
                if(navmeshVertex != null) {
                    vertices.Add(navmeshVertex);
                }
            }

            return vertices.ToArray();
        }
    }
}