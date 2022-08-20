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
            NavMeshAgent = GetComponent<NavMeshAgent>();
            GetComponent<CharacterController>().enabled = false;
            ActorData.IsPlayer = false;
        }
    }
}

