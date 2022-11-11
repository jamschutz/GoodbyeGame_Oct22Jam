using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI.Controller
{
    public class AIController : MonoBehaviour
    {
        public enum State { Idle, FollowPlayer, FollowTarget }
        
        [Header("Movement")]
        public float moveSpeed;
        public float followDistance;

        protected State state;

        protected Transform currentTarget;
        protected Transform player;


        protected void Init()
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
            currentTarget = player;
        }
    }
}

