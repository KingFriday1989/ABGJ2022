using DoubleAgent.Data;
using DoubleAgent.Helpers;
using Helpers.Extensions;
using UnityEngine;
using UnityEngine.AI;

namespace DoubleAgent.Controllers.Actors
{
    [RequireComponent(typeof(Actor))]
    public class ActorInputs : Core.Behaviour
    {
        public WalkingDirections walkingDirection;
        
        private Actor actor;
        
        private void Start()
        {
            actor = GetComponent<Actor>();
        }
        
        private void Update()
        {
            if (GameData.Initialized & GameData.State != GameStates.GameRunning) 
                return;
            else
            {
                if (actor.ActorData.IsPlayer)
                {
                    MouseInput();
                    ActorInput();
                }
                WalkingDirection();
                ActorAnim();
                MovementFloats();
            }
        }

        void MouseInput()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Physics.Raycast(ray, out RaycastHit hitInfo);

            if(hitInfo.collider != null)
            {
                actor.ActorData.MouseTarget = hitInfo.point;
            }
        }

        void ActorInput()
        {
            if (Input.GetKey(KeyCode.W))
            {
                actor.ActorData.moveForward = true;
            }
            else
                actor.ActorData.moveForward = false;

            if (Input.GetKey(KeyCode.S))
                actor.ActorData.moveBackward = true;
            else
                actor.ActorData.moveBackward = false;

            if (Input.GetKey(KeyCode.A))
                actor.ActorData.moveStepLeft = true;
            else
                actor.ActorData.moveStepLeft = false;

            if (Input.GetKey(KeyCode.D))
                actor.ActorData.moveStepRight = true;
            else
                actor.ActorData.moveStepRight = false;


            //Fire Pistol
            if (Input.GetKey(KeyCode.Mouse0))
            {
                actor.ActorData.weapon.Fire();
            }
            //Throw Grenade
            if (Input.GetKey(KeyCode.Mouse1))
            {
                actor.ActorData.weapon.TossGrenade();
            }

            if (Input.GetKey(KeyCode.LeftShift))
                actor.ActorData.Sprint = true;
            else
                actor.ActorData.Sprint = false;

        }

        void WalkingDirection()
        {
            var YRot = transform.rotation.eulerAngles.y;
            var Y = Mathf.Abs(YRot) <= 45 || Mathf.Abs(YRot) >= 135 ? 1:0;
            var XRot = Mathf.Abs(YRot).IsBetween(45,135) ? 1:0;

            if(XRot == 1 & YRot < 0)
            {
                XRot = -1;
            }

            if(Y == 1 && Mathf.Abs(YRot) >= 135)
            {
                Y = -1;
            }

            walkingDirection = Algorithms.GetWalkingDirection((actor.ActorData.moveStepRight) ? 1 : (actor.ActorData.moveStepLeft) ? -1 : 0, (actor.ActorData.moveForward) ? 1 : (actor.ActorData.moveBackward) ? -1 : 0, XRot, Y);
        }

        void ActorAnim()
        {
            actor.ActorData.forward = walkingDirection == WalkingDirections.WalkForward;
            actor.ActorData.backward = walkingDirection == WalkingDirections.WalkBackward;
            actor.ActorData.stepLeft = walkingDirection == WalkingDirections.WalkLeft;
            actor.ActorData.stepRight = walkingDirection == WalkingDirections.WalkRight;
        }

        void MovementFloats()
        {
            actor.ActorData.MovY = Mathf.Lerp(actor.ActorData.MovY, (actor.ActorData.moveForward) ? 1f : (actor.ActorData.moveBackward) ? -1f : 0f, 2f * Time.fixedDeltaTime);
            actor.ActorData.MovX = Mathf.Lerp(actor.ActorData.MovX, (actor.ActorData.moveStepRight) ? 1f : (actor.ActorData.moveStepLeft) ? -1f : 0f, 2f * Time.fixedDeltaTime);

            if (actor.ActorData.MovX > 0.99f)
                actor.ActorData.MovX = 1f;
            else if (actor.ActorData.MovX < -0.99f)
                actor.ActorData.MovX = -1f;
            else if (actor.ActorData.MovX < 0.01f && actor.ActorData.MovX > -0.01f)
                actor.ActorData.MovX = 0f;

            if (actor.ActorData.MovY > 0.99f)
                actor.ActorData.MovY = 1f;
            else if (actor.ActorData.MovY < -0.99f)
                actor.ActorData.MovY = -1f;
            else if (actor.ActorData.MovY < 0.01f && actor.ActorData.MovY > -0.01f)
                actor.ActorData.MovY = 0f;

            actor.ActorData.Speed = Mathf.Lerp(actor.ActorData.Speed,actor.ActorData.Sprint ? 2f : 1.25f, 8f * Time.deltaTime);
            if (actor.ActorData.Speed > 1.99f)
                actor.ActorData.Speed = 2f;
            else if (actor.ActorData.Speed < 1.26f)
                actor.ActorData.Speed = 1.25f;

            if (actor.ActorData.IsPlayer & actor.ActorData.move.magnitude > 0.1f || !actor.ActorData.IsPlayer & Vector3.Magnitude(actor.NavMeshAgent.steeringTarget) > 0)
            {

                actor.ActorData.AnimMovY = Mathf.Lerp(actor.ActorData.AnimMovY, (actor.ActorData.forward) ? 1f : (actor.ActorData.backward) ? -1f : 0f, 8f * Time.fixedDeltaTime);
                actor.ActorData.AnimMovX = Mathf.Lerp(actor.ActorData.AnimMovX, (actor.ActorData.stepRight) ? 1f : (actor.ActorData.stepLeft) ? -1f : 0f, 8f * Time.fixedDeltaTime);
            }
            else
            {
                actor.ActorData.AnimMovY = Mathf.Lerp(actor.ActorData.AnimMovY, 0f, 8f * Time.fixedDeltaTime);
                actor.ActorData.AnimMovX = Mathf.Lerp(actor.ActorData.AnimMovX, 0f, 8f * Time.fixedDeltaTime);
            }
        }

        private bool SetDestination(Vector3 targetDestination)
        {
            NavMeshHit hit;
            if (NavMesh.SamplePosition(targetDestination, out hit, 1f, NavMesh.AllAreas))
            {
                (actor as ActorAI).NavMeshAgent.SetDestination(hit.position);
                return true;
            }
            return false;
        }
    }
}