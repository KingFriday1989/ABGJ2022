using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AnimationState = Controllers.AnimationState;
using Editor;
using Helpers.Extensions;
using Controllers.Actors;

namespace Controllers.States.Actors
{
    public sealed class CharacterAnimator : AnimationStateMachine_Base
    {
        public Actor actor;
        public const string Filename = "Character Animator";

        #region VARIABLE DECLARATIONS
        private AnimationState_Base _DefaultState;
        protected override AnimationState DefaultState => _DefaultState;
        public AnimationState_Base DefaultAnimationState => _DefaultState;

        public CharacterState_Idle characterState_Idle => GetState<CharacterState_Idle>();
        public CharacterState_Walking characterState_Walking => GetState<CharacterState_Walking>();
        public CharacterState_Talking characterState_Talking => GetState<CharacterState_Talking>();

        public new AnimationState_Base CurrentState => base.CurrentState as AnimationState_Base;
        public new AnimationState_Base PreviousState => base.PreviousState as AnimationState_Base;
        public new AnimationState_Base PreviousStateFromHistory(int backCount) => base.PreviousStateFromHistory(backCount) as AnimationState_Base;
        #endregion

        #region Setup
        protected override void Awake()
        {
            actor = GetComponentInParent<Actor>();

            if (gameObject.TryGetComponentInChildren(out CharacterState_Idle idle))
                _DefaultState = idle;
            base.Awake();

            if (gameObject.TryGetComponentInChildren(out CharacterState_Walking walking))
                AddState(walking, false);
            if (gameObject.TryGetComponentInChildren(out CharacterState_Talking talking))
                AddState(talking, false);
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
        private void SetIdle() => SetState<CharacterState_Idle>();

        [InspectorButton("SetWalking", ButtonWidth = 200)]
        [SerializeField] private bool m_SetWalking;
        [ContextMenu("Walking")]
        private void SetWalking() => SetState<CharacterState_Walking>();

        [InspectorButton("SetTalking", ButtonWidth = 200)]
        [SerializeField] private bool m_SetTalking;
        [ContextMenu("Talking")]
        private void SetTalking() => SetState<CharacterState_Talking>();
        #endregion
    }
}
