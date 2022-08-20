using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AnimationState = Controllers.AnimationState;
using Editor;
using Helpers.Extensions;
using Controllers.Actors;

namespace Controllers.States.Actors
{
    public sealed class ActorAnimator : AnimationStateMachine_Base
    {
        public Actor actor;
        public const string Filename = "Character Animator";

        #region VARIABLE DECLARATIONS
        private AnimationState _DefaultState;
        protected override AnimationState DefaultState => _DefaultState;
        public AnimationState DefaultAnimationState => _DefaultState;

        public ActorState_Idle actorState_Idle => GetState<ActorState_Idle>();
        public ActorState_Moving actorState_Moving => GetState<ActorState_Moving>();

        public new AnimationState CurrentState => base.CurrentState;
        public new AnimationState PreviousState => base.PreviousState;
        public new AnimationState PreviousStateFromHistory(int backCount) => base.PreviousStateFromHistory(backCount);
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

        public new ActorState SetState<ActorState>()
            where ActorState : AnimationState_Base
        {
            if (HasState<ActorState>())
            {
                if (TryGetState(out ActorState state))
                {
                    if (!state.CanSetState)
                    {
                        return CurrentState as ActorState;
                    }
                }
                return base.SetState<ActorState>();
            }
            return CurrentState as ActorState;
        }

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
