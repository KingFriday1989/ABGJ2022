using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Data.Actors;
using Controllers.States.Actors;

namespace Controllers.Actors
{
    public abstract class Actor : MonoBehaviour
    {
        public ActorData ActorData;
        public ActorController ActorController;
        public ActorAnimator ActorAnimator;
        public NavMeshAgent NavMeshAgent;

        public virtual void Awake()
        {
            if(ActorData == null)
                ActorData = GetComponent<ActorData>();

            if(ActorController == null)
                ActorController = GetComponent<ActorController>();

            if (ActorAnimator == null)
                ActorAnimator = GetComponent<ActorAnimator>();
            if(NavMeshAgent == null)
                NavMeshAgent = GetComponent<NavMeshAgent>();
        }
        public virtual void Start()
        {

        }
    }
}