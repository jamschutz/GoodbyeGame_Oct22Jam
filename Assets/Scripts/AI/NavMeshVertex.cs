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


        public NavMeshVertex()
        {
            neighbors = new List<NavMeshVertex>();
        }
    }
}