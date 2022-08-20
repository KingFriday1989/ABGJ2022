using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Controllers.Actors
{
    public class ActorAI : Actor
    {
        public NavMeshAgent NavMeshAgent;

        void Awake()
        {
           base.Awake();
        }
        void Start()
        {
            base.Start();
        }
    }
}

