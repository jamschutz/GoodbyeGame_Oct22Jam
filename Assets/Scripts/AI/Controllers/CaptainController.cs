using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace AI.Controller
{
    public class CaptainController : AIController
    {
        public State startingState;

        private void Start()
        {
            base.Init();
            state = startingState;
        }


        private void Update()
        {
            switch(state) {
                case State.Idle:
                    // do nothing
                    break;
                case State.FollowPlayer:
                case State.FollowTarget:
                    MoveTowardsTarget();
                    break;
                default:
                    Debug.LogError($"{gameObject.name} is in unknown state: {state.ToString()}");
                    break;
            }
        }


        public void FollowTarget(Transform target)
        {
            state = State.FollowTarget;
            currentTarget = target;
        }


        public void FollowPlayer()
        {
            state = State.FollowPlayer;
            currentTarget = player;
        }


        public void GoIdle()
        {
            state = State.Idle;
        }
    }

}
