using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Controllers.Actors
{
    public class ActorPlayer : Actor
    {
        public CharacterController CharacterController;

        private void Awake()
        {
            base.Awake();
            CharacterController = GetComponent<CharacterController>();
            GetComponent<NavMeshAgent>().enabled = false;
            ActorData.IsPlayer = true;
        }
    }
}

