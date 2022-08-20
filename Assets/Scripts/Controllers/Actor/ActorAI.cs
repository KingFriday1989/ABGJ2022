using UnityEngine;
using UnityEngine.AI;

namespace DoubleAgent.Controllers.Actors
{
    public class ActorAI : Actor
    {
        protected override void Awake()
        {
            base.Awake();
            NavMeshAgent = GetComponent<NavMeshAgent>();
            GetComponent<CharacterController>().enabled = false;
            ActorData.IsPlayer = false;
        }
    }
}