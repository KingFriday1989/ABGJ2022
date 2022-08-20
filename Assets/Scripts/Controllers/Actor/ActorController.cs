using DoubleAgent.Controllers.States.Actors;
using Helpers;
using UnityEngine;

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

            //AnimationState();
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
            Debug.Log(movePos);
            (actor as ActorPlayer).CharacterController.Move(movePos);
            RotateCharacter();
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
            actor.transform.forward = actor.ActorData.move;
        }

        public static bool isGrounded(Transform transform, CharacterController characterController)
        {
            Vector3 Position = transform.position + new Vector3(0, characterController.height / 2f, 0);
            float Radius = characterController.radius;
            float Height = characterController.height;
            if (SphereCheck(Position, -transform.up, Radius, Height / 2 - Radius + 0.1f, LayerMask.GetMask("Geometry")) || SphereCheck(Position, -transform.up, Radius, Height / 2 - Radius + 0.1f, LayerMask.GetMask("Terrain")))
                return true;
            else
                return false;
        }

        public static bool SphereCheck(Vector3 Position, Vector3 Direction, float Radius, float Distance, LayerMask layerMask)
        {
            return Physics.SphereCast(Position, Radius, Direction, out RaycastHit hit, Distance, layerMask);
        }
    }
}