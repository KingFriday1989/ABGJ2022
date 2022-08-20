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
            else
                ActorNavInput();

            MovementFloats();
        }

        void MouseInput()
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                Physics.Raycast(ray, out RaycastHit hitInfo);
                actor.ActorData.ClickTarget = hitInfo.point;

                //SetDestination(actor.ActorData.ClickTarget);
            }
        }

        void ActorInput()
        {
            if (Input.GetKey(KeyCode.W))
            {
                actor.ActorData.forward = true;
            }
            else
                actor.ActorData.forward = false;

            if (Input.GetKey(KeyCode.S))
                actor.ActorData.backward = true;
            else
                actor.ActorData.backward = false;

            if (Input.GetKey(KeyCode.A))
                actor.ActorData.stepLeft = true;
            else
                actor.ActorData.stepLeft = false;

            if (Input.GetKey(KeyCode.D))
                actor.ActorData.stepRight = true;
            else
                actor.ActorData.stepRight = false;
            //Fire Pistol
            if(Input.GetKey(KeyCode.Mouse0))
            {
                actor.ActorData.pistol.Fire();
            }
            //Throw Grenade
            if (Input.GetKey(KeyCode.Mouse1))
            {
                actor.ActorData.grenade.Fire();
            }

            
        }

        void ActorNavInput()
        {
            if (actor.ActorData.NavMeshAgent.velocity != Vector3.zero)
            {
                //geting some angles based upon where the actor is going and where the actor is looking
                var verticalMovement = Vector3.SignedAngle(actor.ActorData.NavMeshAgent.steeringTarget - transform.position, transform.forward, Vector3.up);
                var horizontalMovement = Vector3.SignedAngle(actor.ActorData.NavMeshAgent.steeringTarget - transform.position, transform.right, Vector3.up);

                if (verticalMovement < 45 && verticalMovement >= 0 || verticalMovement > -45 && verticalMovement <= 0)
                {
                    actor.ActorData.forward = true;
                    actor.ActorData.backward = false;
                }
                else if (verticalMovement > 135 && verticalMovement <= 180 || verticalMovement < -135 && verticalMovement >= -180)
                {
                    actor.ActorData.forward = false;
                    actor.ActorData.backward = true;
                }
                else
                {
                    actor.ActorData.forward = false;
                    actor.ActorData.backward = false;
                }

                if (horizontalMovement < 45 && horizontalMovement >= 0 || horizontalMovement > -45 && horizontalMovement <= 0)
                {
                    actor.ActorData.stepLeft = true;
                    actor.ActorData.stepRight = false;
                }
                else if (horizontalMovement > 135 && horizontalMovement <= 180 || horizontalMovement < -135 && horizontalMovement >= -180)
                {
                    actor.ActorData.stepLeft = false;
                    actor.ActorData.stepRight = true;
                }
                else
                {
                    actor.ActorData.stepLeft = false;
                    actor.ActorData.stepRight = false;
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
            actor.ActorData.MovY = Mathf.Lerp(actor.ActorData.MovY, (actor.ActorData.forward) ? 1f : (actor.ActorData.backward) ? -1f : 0f, 2f * Time.fixedDeltaTime);
            actor.ActorData.MovX = Mathf.Lerp(actor.ActorData.MovX, (actor.ActorData.stepRight) ? 1f : (actor.ActorData.stepLeft) ? -1f : 0f, 2f * Time.fixedDeltaTime);

            if (actor.ActorData.MovX > 0.99f)
            {
                actor.ActorData.MovX = 1f;
            }
            else if (actor.ActorData.MovX < -0.99f)
            {
                actor.ActorData.MovX = -1f;
            }
            else if (actor.ActorData.MovX < 0.01f && actor.ActorData.MovX > -0.01f)
            {
                actor.ActorData.MovX = 0f;
            }

            if (actor.ActorData.MovY > 0.99f)
            {
                actor.ActorData.MovY = 1f;
            }
            else if (actor.ActorData.MovY < -0.99f)
            {
                actor.ActorData.MovY = -1f;
            }
            else if (actor.ActorData.MovY < 0.01f && actor.ActorData.MovY > -0.01f)
            {
                actor.ActorData.MovY = 0f;
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