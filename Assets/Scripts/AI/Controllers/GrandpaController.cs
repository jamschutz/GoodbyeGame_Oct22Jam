using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI.Controller
{
    public class GrandpaController : MonoBehaviour
    {
        [Header("Movement")]
        public float moveSpeed = 2;
        public float pollPlayerPositionInterval = 3.0f;

        [Header("Catch radius")]
        public float catchPlayerRadius = 5;


        private Transform player;
        private Queue<Vector2> waypoints;


        private void Start()
        {
            // get player
            player = GameObject.FindGameObjectWithTag("Player").transform;

            // initialize waypoints to player position
            waypoints = new Queue<Vector2>();
            waypoints.Enqueue(player.position);

            // repeatedly poll player position
            InvokeRepeating("AddPlayerPositionToWaypoints", pollPlayerPositionInterval, pollPlayerPositionInterval);
        }


        private void Update()
        {
            // move towards current waypoint
            MoveTowardsDestination();

            // if(IsPlayerInCatchRadius()) {
            //     // deactivate player controller
            //     Debug.Log($"game over!!!");
            //     player.GetComponent<PlayerController>().enabled = false;
            // }
        }


        private bool IsPlayerInCatchRadius()
        {
            return Vector2.Distance(transform.position, player.position) < catchPlayerRadius;
        }



        private void AddPlayerPositionToWaypoints()
        {
            waypoints.Enqueue(player.position);
        }


        private void MoveTowardsDestination()
        {
            // if queue empty, just wait
            if(waypoints.Count == 0) return;

            // get closest node to move towards
            var currentTarget = waypoints.Peek();

            // check if we've reached current target
            if(Vector2.Distance(currentTarget, transform.position) <= 0.1f) {
                // and if so, set target to the next node
                waypoints.Dequeue();
                if(waypoints.Count == 0) {
                    return;
                }

                currentTarget = waypoints.Peek();
            }
            

            // move!
            var moveDirection = (currentTarget - new Vector2(transform.position.x, transform.position.y)).normalized;
            transform.Translate(moveDirection * moveSpeed * Time.deltaTime, Space.World);
        }
    }

}
