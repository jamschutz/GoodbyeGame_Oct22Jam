using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Linq;

namespace AI
{
    public class NavMeshVertex
    {
        public Vector2 position;
        public List<NavMeshVertex> neighbors;
        // just used for A* algorithm
        public float searchScore;
        public int id;


        public NavMeshVertex()
        {
            neighbors = new List<NavMeshVertex>();
        }
    }
}