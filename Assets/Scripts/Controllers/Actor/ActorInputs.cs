using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


namespace DoubleAgent.Controllers.Actors
{
    public class ActorInputs : MonoBehaviour
    {
        public Actor actor;
        
        private void Start()
        {
            actor = GetComponent<Actor>();
        }
        
        private void Update()
        {
            MouseInput();
            if (actor.ActorData.IsPlayer)
                ActorInput();

            ActorAnim();
            MovementFloats();
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
                actor.ActorData.pistol.Fire();
            }
            //Throw Grenade
            if (Input.GetKey(KeyCode.Mouse1))
            {
                actor.ActorData.grenade.Fire();
            }

            if (Input.GetKey(KeyCode.LeftShift))
                actor.ActorData.Sprint = true;
            else
                actor.ActorData.Sprint = false;

        }

        void ActorAnim()
        {
            if (actor)
            {
                var verticalMovement = Vector3.SignedAngle(actor.ActorData.MouseTarget - transform.position, transform.forward, Vector3.up);
                var horizontalMovement = Vector3.SignedAngle(actor.ActorData.MouseTarget - transform.position, transform.right, Vector3.up);
                var actorData = actor.ActorData;
                //forward
                if (verticalMovement < 45 && verticalMovement >= 0 || verticalMovement > -45 && verticalMovement <= 0)
                {
                    if (actorData.moveForward)
                    {
                        actor.ActorData.forward = true;
                        actor.ActorData.backward = false;
                        actor.ActorData.stepLeft = false;
                        actor.ActorData.stepRight = false;
                    }
                    else if (actorData.moveBackward)
                    {
                        actor.ActorData.forward = false;
                        actor.ActorData.backward = true;
                        actor.ActorData.stepLeft = false;
                        actor.ActorData.stepRight = false;
                    }
                    else if (actorData.moveStepLeft)
                    {
                        actor.ActorData.forward = false;
                        actor.ActorData.backward = false;
                        actor.ActorData.stepLeft = true;
                        actor.ActorData.stepRight = false;
                    }
                    else if (actorData.moveStepRight)
                    {
                        actor.ActorData.forward = false;
                        actor.ActorData.backward = false;
                        actor.ActorData.stepLeft = false;
                        actor.ActorData.stepRight = true;
                    }
                }
                //backward
                else if (verticalMovement > 135 && verticalMovement <= 180 || verticalMovement < -135 && verticalMovement >= -180)
                {
                    if (actorData.moveForward)
                    {
                        actor.ActorData.forward = false;
                        actor.ActorData.backward = true;
                        actor.ActorData.stepLeft = false;
                        actor.ActorData.stepRight = false;
                    }
                    else if (actorData.moveBackward)
                    {
                        actor.ActorData.backward = true;
                        actor.ActorData.forward = false;
                        actor.ActorData.stepLeft = false;
                        actor.ActorData.stepRight = false;
                    }
                    else if (actorData.moveStepLeft)
                    {
                        actor.ActorData.forward = false;
                        actor.ActorData.backward = false;
                        actor.ActorData.stepLeft = false;
                        actor.ActorData.stepRight = true;
                    }
                    else if (actorData.moveStepRight)
                    {
                        actor.ActorData.forward = false;
                        actor.ActorData.backward = false;
                        actor.ActorData.stepLeft = true;
                        actor.ActorData.stepRight = false;
                    }
                }

                //left
                if (horizontalMovement < 45 && horizontalMovement >= 0 || horizontalMovement > -45 && horizontalMovement <= 0)
                {
                    if (actorData.moveForward)
                    {
                        actor.ActorData.forward = false;
                        actor.ActorData.backward = false;
                        actor.ActorData.stepLeft = false;
                        actor.ActorData.stepRight = true;
                    }
                    else if (actorData.moveBackward)
                    {
                        actor.ActorData.forward = false;
                        actor.ActorData.backward = false;
                        actor.ActorData.stepLeft = true;
                        actor.ActorData.stepRight = false;

                    }
                    else if (actorData.moveStepLeft)
                    {
                        actor.ActorData.forward = true;
                        actor.ActorData.backward = false;
                        actor.ActorData.stepLeft = false;
                        actor.ActorData.stepRight = false;
                    }
                    else if (actorData.moveStepRight)
                    {
                        actor.ActorData.forward = false;
                        actor.ActorData.backward = true;
                        actor.ActorData.stepLeft = false;
                        actor.ActorData.stepRight = false;
                    }
                }
                //right
                else if (horizontalMovement > 135 && horizontalMovement <= 180 || horizontalMovement < -135 && horizontalMovement >= -180)
                {
                    if (actorData.moveForward)
                    {
                        actor.ActorData.forward = false;
                        actor.ActorData.backward = false;
                        actor.ActorData.stepLeft = true;
                        actor.ActorData.stepRight = false;
                    }
                    else if (actorData.moveBackward)
                    {
                        actor.ActorData.forward = false;
                        actor.ActorData.backward = false;
                        actor.ActorData.stepLeft = false;
                        actor.ActorData.stepRight = true;
                    }
                    else if (actorData.moveStepLeft)
                    {
                        actor.ActorData.forward = false;
                        actor.ActorData.backward = true;
                        actor.ActorData.stepLeft = false;
                        actor.ActorData.stepRight = false;
                    }
                    else if (actorData.moveStepRight)
                    {
                        actor.ActorData.forward = true;
                        actor.ActorData.backward = false;
                        actor.ActorData.stepLeft = false;
                        actor.ActorData.stepRight = false;
                    }
                }
            }
            else
            {
                actor.ActorData.forward = false;
                actor.ActorData.backward = false;
                actor.ActorData.stepLeft = false;
                actor.ActorData.stepRight = false;
            }
        }
        

        void MovementFloats()
        {
            actor.ActorData.MovY = Mathf.Lerp(actor.ActorData.MovY, (actor.ActorData.moveForward) ? 1f : (actor.ActorData.moveBackward) ? -1f : 0f, 2f * Time.fixedDeltaTime);
            actor.ActorData.MovX = Mathf.Lerp(actor.ActorData.MovX, (actor.ActorData.moveStepRight) ? 1f : (actor.ActorData.moveStepLeft) ? -1f : 0f, 2f * Time.fixedDeltaTime);

            if(actor.ActorData.move.magnitude > 0.1f)
            {

                actor.ActorData.AnimMovY = Mathf.Lerp(actor.ActorData.AnimMovY, (actor.ActorData.forward) ? 1f : (actor.ActorData.backward) ? -1f : 0f, 8f * Time.fixedDeltaTime);
                actor.ActorData.AnimMovX = Mathf.Lerp(actor.ActorData.AnimMovX, (actor.ActorData.stepRight) ? 1f : (actor.ActorData.stepLeft) ? -1f : 0f, 8f * Time.fixedDeltaTime);
            }
            else
            {
                actor.ActorData.AnimMovY = Mathf.Lerp(actor.ActorData.AnimMovY, 0f, 8f * Time.fixedDeltaTime);
                actor.ActorData.AnimMovX = Mathf.Lerp(actor.ActorData.AnimMovX, 0f, 8f * Time.fixedDeltaTime);
            }

            actor.ActorData.Speed = Mathf.Lerp(actor.ActorData.Speed,(actor.ActorData.Sprint) ? 1.5f : 1f, 2f * Time.deltaTime);

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

            if (actor.ActorData.Speed > 1.49f)
                actor.ActorData.Speed = 1.5f;
            else if (actor.ActorData.Speed < 1.01f)
                actor.ActorData.Speed = 1f;

            //var mov = Mathf.Max(Mathf.Abs(actor.ActorData.MovY), Mathf.Abs(actor.ActorData.MovX));
            //actor.ActorData.Mov = mov;
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