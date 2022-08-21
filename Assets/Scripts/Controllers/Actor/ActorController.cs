using DoubleAgent.Controllers.States.Actors;
using Helpers;
using RayFire;
using UnityEngine;

namespace DoubleAgent.Controllers.Actors
{
    public class ActorController : Core.Behaviour
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
            RotateCharacter(transform, actor.ActorData.MouseTarget);
        }
        void RotateCharacter(Transform transform, Vector3 target)
        {
            var lerp = Vector3.Slerp(transform.forward, target - transform.position, Time.deltaTime * 8);
            transform.forward = lerp;

            var euler = transform.rotation.eulerAngles;
            euler.x = 0;
            euler.z = 0;
            transform.rotation = Quaternion.Euler(euler);
        }

        void AnimationState()
        {
            bool isMoving = actor.ActorData.IsPlayer ? actor.ActorData.MovX != 0 || actor.ActorData.MovY != 0 : actor.NavMeshAgent.steeringTarget.magnitude > 0;
            if (isMoving)
            {
                if (!actor.ActorAnimator.IsCurrentState<ActorState_Moving>())
                    actor.ActorAnimator.actorState_Moving.Begin();

                if (!actor.ActorData.IsPlayer)
                {
                    RotateCharacter(transform, actor.NavMeshAgent.steeringTarget);
                }
            }
            else if (!actor.ActorAnimator.IsCurrentState<ActorState_Idle>())
                actor.ActorAnimator.ReturnToDefaultState();
        }
        
        public void DisableDynamite()
        {
            actor.ActorData.ItemL.gameObject.SetActive(false);
        }
        public void EnableDynamite()
        {
            actor.ActorData.ItemL.gameObject.SetActive(true);
        }
        public void SpawnDynamite()
        {
            var bomb = Instantiate(actor.ActorData.bombPrefab);
            var rb = bomb.GetComponent<Rigidbody>();
            rb.isKinematic = true;
            bomb.transform.position = transform.position + transform.forward + new Vector3(0,1.5f,0);
            rb.isKinematic = false;
            rb.useGravity = true;
            rb.velocity = transform.forward * 8 + transform.up * 4;
        }

        [ContextMenu("KillActor")]
        public void KillActor()
        {
            actor.ActorAnimator.animator.enabled = false;
            actor.ActorAnimator.gameObject.SetActive(false);
            GetComponent<ActorInputs>().enabled = false;
            actor.NavMeshAgent.enabled = false;
            actor.CharacterController.enabled = false;


            foreach (var collider in actor.ActorData.ragdollColliders)
            {
                collider.enabled = true;
            }
            foreach (var rb in actor.ActorData.ragdollRigidbodies)
            {
                rb.isKinematic = false;
                rb.useGravity = true;
                rb.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
            }
        }

        public static bool isGrounded(Transform transform, CharacterController characterController)
        {
            Vector3 Position = transform.position + new Vector3(0,0.1f,0);
            float Legnth = 0.4f ;
            if(RayCheck(Position,-transform.up,Legnth))
                return true;
            else
                return false;
        }

        public static bool RayCheck(Vector3 Position, Vector3 Direction, float Distance)
        {
            var cast = Physics.Raycast(Position, Direction, out RaycastHit hitInfo, Distance, LayerMask.GetMask("Terrain"));

            if (hitInfo.collider != null)
                return true;
            else
                return false;
        }
    }
}