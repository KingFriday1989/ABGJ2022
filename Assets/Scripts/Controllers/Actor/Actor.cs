using Data.Actors;
using DoubleAgent.Controllers.States.Actors;
using UnityEngine.AI;

namespace DoubleAgent.Controllers.Actors
{
    public abstract class Actor : Core.Behaviour
    {
        public ActorData ActorData;
        public ActorController ActorController;
        public ActorAnimator ActorAnimator;
        public NavMeshAgent NavMeshAgent;

        protected override void Awake()
        {
            base.Awake();

            if(ActorData == null)
                ActorData = GetComponent<ActorData>();

            if(ActorController == null)
                ActorController = GetComponent<ActorController>();

            if (ActorAnimator == null)
                ActorAnimator = GetComponent<ActorAnimator>();
        }

        public virtual void Start()
        {

        }
    }
}