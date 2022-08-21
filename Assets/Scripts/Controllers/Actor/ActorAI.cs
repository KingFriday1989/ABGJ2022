using Controllers;
using Helpers;
using Helpers.Extensions;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;

namespace DoubleAgent.Controllers.Actors
{
    public class ActorAI : Actor
    {
        [SerializeField] private float RemainingDistanceThreshold = 0.1f;
        public Waypoint waypoint;

        private Waypoint currentWaypoint;

        protected override void Awake()
        {
            base.Awake();
            CharacterController.enabled = false;
            ActorData.IsPlayer = false;
            currentWaypoint = waypoint;
        }

        public override async void Start()
        {
            base.Start();
            Initialized = true;
            await MoveToPosition(currentWaypoint.position);
        }

        public NavMeshPath CurrentPath => NavMeshAgent.path;
        public virtual Vector3 PathDestination => NavMeshAgent.pathEndPosition;
        public virtual float RemainingPathDistance => NavMeshAgent.remainingDistance;

        private bool destinationSet;

        public virtual async Task MoveToPosition(Vector3 position)
        {
            destinationSet = false;
            await Timer.WaitForFrame(); //Wait for any running tasks to end

            bool validPositon = NavMeshAgent.SetDestination(position);
            if (!validPositon) return;
            await Timer.WaitWhile(() => this && NavMeshAgent.pathPending);
            if (this == null) return;
            if (NavMeshAgent.pathStatus == NavMeshPathStatus.PathInvalid) return;

            //Move to destination
            destinationSet = true;
            if (NavMeshAgent.isStopped) //If we are currently paused, resume movement
                ResumePathMovement();
            while (this && destinationSet && RemainingPathDistance > RemainingDistanceThreshold)
            {
                //if the current path has become stale we need to recalculate
                if (NavMeshAgent.isPathStale)
                {
                    NavMeshPath newPath = new NavMeshPath();
                    if (!NavMeshAgent.CalculatePath(position, newPath))
                        break;
                    if (!ChangeActivePath(newPath))
                        break;
                }

                //The current path has become invalid
                if (CurrentPath.status == NavMeshPathStatus.PathInvalid)
                    break;

                await Timer.WaitForFrame();
            }
            if (this == null) return;

            //Destination was reached
            //if (transform.position == PathDestination)
            //{
                //DestinationReached?.Invoke(PathDestination); //event
                OnDestinationReached(PathDestination); //engine
            //}
        }

        public virtual async void OnDestinationReached(Vector3 position)
        {
            currentWaypoint = currentWaypoint.Traverse();
            await MoveToPosition(currentWaypoint.position);
        }

        public bool ChangeActivePath(NavMeshPath newPath)
        {
            if (NavMeshAgent == null) return false;
            return NavMeshAgent.SetPath(newPath);
        }

        public void ResumePathMovement()
        {
            NavMeshAgent.IfNotNull(_ => NavMeshAgent.isStopped = false);
            //MovementResumed?.Invoke(); //event
            //OnMovementResumed(); //engine
        }
    }
}