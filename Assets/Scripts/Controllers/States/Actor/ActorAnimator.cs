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

        public ActorState_Idle characterState_Idle => GetState<ActorState_Idle>();
        public ActorState_Moving characterState_Walking => GetState<ActorState_Moving>();
        //public CharacterState_Talking characterState_Talking => GetState<CharacterState_Talking>();

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

        public new CharacterState SetState<CharacterState>()
            where CharacterState : AnimationState_Base
        {
            if (HasState<CharacterState>())
            {
                if (TryGetState(out CharacterState state))
                {
                    if (!state.CanSetState)
                    {
                        return CurrentState as CharacterState;
                    }
                }
                return base.SetState<CharacterState>();
            }
            return CurrentState as CharacterState;
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

        //[InspectorButton("SetTalking", ButtonWidth = 200)]
        //[SerializeField] private bool m_SetTalking;
        //[ContextMenu("Talking")]
        //private void SetTalking() => SetState<CharacterState_Talking>();
        #endregion
    }
}