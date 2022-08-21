using Data.Actors;
using DoubleAgent.Controllers.States.Actors;
using UnityEngine.AI;
using UnityEngine;

namespace DoubleAgent.Controllers.Actors
{
    //[RequireComponent(typeof(ActorData)]
    public abstract class Actor : Core.Behaviour
    {
        public ActorData ActorData;
        public ActorController ActorController;
        public ActorAnimator ActorAnimator;
        public NavMeshAgent NavMeshAgent;
        public CharacterController CharacterController;

        protected override void Awake()
        {
            base.Awake();

            if(ActorData == null)
                ActorData = GetComponent<ActorData>();

            if(ActorController == null)
                ActorController = GetComponent<ActorController>();

            if (ActorAnimator == null)
                ActorAnimator = GetComponent<ActorAnimator>();

            if(CharacterController == null)
                CharacterController = GetComponent<CharacterController>();

            if(NavMeshAgent == null)
                NavMeshAgent = GetComponent<NavMeshAgent>();
        }

        public virtual void Start()
        {

        }
    }
}