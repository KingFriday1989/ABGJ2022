using DoubleAgent.Controllers.States.Actors;
using DoubleAgent.Data.Actors;
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

            ActorData.ragdollColliders.Clear();
            var root = transform.Find("Root");
            var colliders = root.GetComponentsInChildren<Collider>();
            for (int i = 0; i < colliders.Length; i++)
            {
                ActorData.ragdollColliders.Add(colliders[i]);
            }

            foreach(var collider in colliders)
            {
                collider.enabled = false;
            }

            ActorData.ragdollRigidbodies.Clear();
            var rbs = root.GetComponentsInChildren<Rigidbody>();
            for (int i = 0; i < rbs.Length; i++)
            {
                ActorData.ragdollRigidbodies.Add(rbs[i]);
            }

            foreach (var rb in rbs)
            {
                rb.isKinematic = true;
            }
        }

        public virtual void Start()
        {

        }
    }
}