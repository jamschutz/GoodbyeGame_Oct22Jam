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
                    MoveTowardsTarget();
                    break;
                default:
                    Debug.LogError($"{gameObject.name} is in unknown state: {state.ToString()}");
                    break;
            }
        }
    }

}
