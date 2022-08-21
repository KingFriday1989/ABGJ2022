using UnityEngine;
using UnityEngine.AI;

namespace DoubleAgent.Controllers.Actors
{
    public class ActorAI : Actor
    {
        protected override void Awake()
        {
            base.Awake();
            CharacterController.enabled = false;
            ActorData.IsPlayer = false;
        }
    }
}