using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI
{
    public class NavMesh : MonoBehaviour
    {
        [Header("Vertices")]
        public float maxDistanceBetweenVertices = 5;
        public Transform[] vertices;


        public Vector3[] GetPathBetweenPoints(Vector3 start, Vector3 end)
        {
            var path = new List<Vector3>();

            return path.ToArray();
        }
    }
}

