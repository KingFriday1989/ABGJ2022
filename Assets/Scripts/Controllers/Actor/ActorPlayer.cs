using UnityEngine;
using UnityEngine.AI;

namespace DoubleAgent.Controllers.Actors
{
    public class ActorPlayer : Actor
    {
        public CharacterController CharacterController;

        protected override void Awake()
        {
            base.Awake();
            CharacterController = GetComponent<CharacterController>();
            GetComponent<NavMeshAgent>().enabled = false;
            ActorData.IsPlayer = true;
        }
    }
}