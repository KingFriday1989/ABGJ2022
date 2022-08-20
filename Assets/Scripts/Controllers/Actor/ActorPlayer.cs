using UnityEngine;
using UnityEngine.AI;

namespace DoubleAgent.Controllers.Actors
{
    public class ActorPlayer : Actor
    {
        protected override void Awake()
        {
            base.Awake();
            NavMeshAgent.enabled = false;
            ActorData.IsPlayer = true;
        }
    }
}