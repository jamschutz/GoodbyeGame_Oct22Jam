using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI.Controller
{
    [RequireComponent(typeof(AI.Controller.NavigationController))]
    public class MomController : MonoBehaviour
    {
        [Header("Movement")]
        public Transform[] waypoints;


        private Vector2 currentTarget;
        private NavigationController navigationController;


        private void Start()
        {
            navigationController = GetComponent<NavigationController>();

            Invoke("MoveToNextWaypoint", 0.1f);
            // MoveToNextWaypoint();
        }


        private void Update()
        {
            if(IsAtWaypoint()) {
                MoveToNextWaypoint();
            }
        }


        private void MoveToNextWaypoint()
        {
            // move towards current waypoint
            currentTarget = waypoints[Random.Range(0, waypoints.Length)].position;
            Debug.Log($"{gameObject.name}: moving to next waypoint: {currentTarget.ToString()}");
            navigationController.MoveToDestination(currentTarget);
        }


        private bool IsAtWaypoint()
        {
            return Vector2.Distance(transform.position, currentTarget) < 0.5f;
        }
    }

}
