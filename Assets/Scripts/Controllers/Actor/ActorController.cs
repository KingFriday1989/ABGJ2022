using DoubleAgent.Controllers.States.Actors;
using Helpers;
using UnityEngine;
using Helpers;

namespace DoubleAgent.Controllers.Actors
{
    public class ActorController : MonoBehaviour
    {
        public Actor actor;

        void Start()
        {
            if(actor == null)
                actor = GetComponent<Actor>();
        }

        void Update()
        {
            if (actor.ActorData.IsPlayer)
                DoMovePlayer();

            //ActorAnim();
            AnimationState();
        }

        void DoMovePlayer()
        {
            if (isGrounded(transform, (actor as ActorPlayer).CharacterController))
            {
                if (!actor.ActorData.isOnGround)
                    actor.ActorData.isOnGround = true;

                actor.ActorData.move = Vector3.right * actor.ActorData.MovX + Vector3.forward * actor.ActorData.MovY;
                actor.ActorData.velocity = Converter.ChangeY(actor.ActorData.velocity, -6f);
            }
            else
            {
                if (actor.ActorData.isOnGround)
                {
                    actor.ActorData.isOnGround = false;
                    actor.ActorData.velocity = Converter.ChangeY(actor.ActorData.velocity, 0);
                }

                actor.ActorData.velocity = Converter.AddY(actor.ActorData.velocity, -9.81f * 2 * Time.fixedDeltaTime);
            }

            actor.ActorData.move = Vector3.ClampMagnitude(actor.ActorData.move, 1);
            var movePos = actor.ActorData.move * actor.ActorData.Speed * Time.fixedDeltaTime + actor.ActorData.velocity * Time.fixedDeltaTime;
            (actor as ActorPlayer).CharacterController.Move(movePos);
            //RotateCharacter();
        }
        
        void AnimationState()
        {
            bool isMoving = actor.ActorData.MovX != 0 || actor.ActorData.MovY != 0;
            if (isMoving)
            {
                if (/*GameData.State != GameState.InDialogue && */!actor.ActorAnimator.IsCurrentState<ActorState_Moving>())
                {
                    actor.ActorAnimator.actorState_Moving.Begin();
                }
            }
            else if (/*GameData.State != GameState.InDialogue && */!actor.ActorAnimator.IsCurrentState<ActorState_Idle>())
            {
                actor.ActorAnimator.ReturnToDefaultState();
            }
        }

        void RotateCharacter()
        {
            var target = actor.ActorData.MouseTarget;
            var lerp = Vector3.Slerp(transform.forward, target - transform.position, Time.deltaTime );
            transform.forward = lerp;

            var euler = transform.rotation.eulerAngles;
            euler.x = 0;
            euler.z = 0;
            transform.rotation = Quaternion.Euler(euler);
        }

        public static bool isGrounded(Transform transform, CharacterController characterController)
        {
            Vector3 Position = transform.position + new Vector3(0,0.1f,0);
            float Legnth = 0.2f ;
            if(RayCheck(Position,-transform.up,Legnth))
                return true;
            else
                return false;
        }

        public static bool RayCheck(Vector3 Position, Vector3 Direction, float Distance)
        {
            var cast = Physics.Raycast(Position, Direction, out RaycastHit hitInfo, Distance);

            if (hitInfo.collider != null)
                return true;
            else
                return false;
        }
    }
}