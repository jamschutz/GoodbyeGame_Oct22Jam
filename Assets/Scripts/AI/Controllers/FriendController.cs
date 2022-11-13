using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI.Controller
{
    [RequireComponent(typeof(AI.Controller.NavigationController))]
    public class FriendController : MonoBehaviour
    {
        [Header("Movement")]
        public Transform[] waypoints;


        private int currentWaypoint;
        private Vector2 currentTarget;
        private NavigationController navigationController;


        private void Start()
        {
            currentWaypoint = 0;
            navigationController = GetComponent<NavigationController>();

            MoveToNextWaypoint();
        }


        private void Update()
        {
            if(IsAtWaypoint()) {
                MoveToNextWaypoint();
            }
        }


        private void MoveToNextWaypoint()
        {
            Debug.Log("moving to next waypoint!");
            // move towards current waypoint
            currentTarget = waypoints[currentWaypoint].position;
            navigationController.MoveToDestination(currentTarget);

            // increment currentwaypoint with wrap around
            currentWaypoint = (currentWaypoint + 1) % waypoints.Length;
        }


        private bool IsAtWaypoint()
        {
            return Vector2.Distance(transform.position, currentTarget) < 0.5f;
        }
    }

}
