using DoubleAgent.Controllers.Actors;
using Editor;
using Helpers.Extensions;
using UnityEngine;

namespace DoubleAgent.Controllers.States.Actors
{
    public sealed class ActorAnimator : AnimationStateMachine_Base
    {
        public Actor actor;
        public const string Filename = "Character Animator";

        #region VARIABLE DECLARATIONS
        private AnimationState_Base _DefaultState;
        protected override AnimationState_Base DefaultState => _DefaultState;
        public AnimationState_Base DefaultAnimationState => _DefaultState;

        public ActorState_Idle actorState_Idle => GetState<ActorState_Idle>();
        public ActorState_Moving actorState_Moving => GetState<ActorState_Moving>();
        #endregion

        #region Setup
        protected override void Awake()
        {
            actor = GetComponentInParent<Actor>();

            if (gameObject.TryGetComponentInChildren(out ActorState_Idle idle))
                _DefaultState = idle;
            if (gameObject.TryGetComponentInChildren(out ActorState_Moving walking))
                AddState(walking, false);
            base.Awake();
        }

        void Start()
        {
            if (CurrentState != _DefaultState)
                ReturnToDefaultState();
        }
        #endregion

        #region Debug
        [Header("Debugging")]

        [InspectorButton("SetIdle", ButtonWidth = 200)]
        [SerializeField] private bool m_SetIdle;
        [ContextMenu("Idle")]
        private void SetIdle() => SetState<ActorState_Idle>();

        [InspectorButton("SetMoving", ButtonWidth = 200)]
        [SerializeField] private bool m_SetWalking;
        [ContextMenu("Moving")]
        private void SetMoving() => SetState<ActorState_Moving>();
        #endregion
    }
}