using UnityEngine;
using UnityEngine.AI;
using Controllers;
using Helpers;

namespace DoubleAgent.Controllers.Actors
{
    public class ActorAI : Actor
    {
        public Waypoint waypoint;
        private Waypoint currentWaypoint;

        protected override void Awake()
        {
            base.Awake();
            CharacterController.enabled = false;
            ActorData.IsPlayer = false;
            currentWaypoint = waypoint;
            StartWaypoint();
        }

        private void Update()
        {
            Debug.Log(currentWaypoint);
            if(NavMeshAgent.remainingDistance <= 0.25f)
            {
                currentWaypoint = waypoint.Traverse();
                NavMeshAgent.SetDestination(currentWaypoint.position);
            }
        }

        public void StartWaypoint()
        {
            NavMeshAgent.SetDestination(currentWaypoint.position);

            //while (this & NavMeshAgent.remainingDistance > 0.1f)
            //{
            //    await Timer.WaitForFrame();
            //}
            //currentWaypoint = currentWaypoint.Traverse();
            //StartWaypoint();
        }
    }
}