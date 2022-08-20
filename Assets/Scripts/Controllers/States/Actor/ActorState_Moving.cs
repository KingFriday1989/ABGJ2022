using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AnimationState = Controllers.AnimationState;
using Editor;
using Helpers.Extensions;
using Controllers.Actors;

namespace Controllers.States.Actors
{
    public class ActorState_Moving : AnimationState_Base
    {
        public const string Filename = "Walking";
        private Actor actor;

        protected override bool CanInitiate
        {
            get
            {
                if (ActiveStateMachine != null)
                {
                    if (ActiveStateMachine.CurrentState == this) return false;
                    return ActiveStateMachine.IsCurrentState<ActorState_Idle>();
                }
                return false;
            }
        }

        //On Start
        protected override void OnPlayAnimation(AnimationStateMachine stateMachine, string animationKey)
        {
            actor = GetStateMachine<ActorAnimator>().actor;
        }

        //Update
        protected override void OnActiveInStateMachine(AnimationStateMachine stateMachine)
        {
            if (AnimationController != null)
            {
                AnimationController.SetFloat("MovY", actor.ActorData.MovY);
                AnimationController.SetFloat("Horizontal", actor.ActorData.MovX);
            }
        }

        protected override void OnStateCanceled(AnimationStateMachine stateMachine)
        {
        }
    }
}
