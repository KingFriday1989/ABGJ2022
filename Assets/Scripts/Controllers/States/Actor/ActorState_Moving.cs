using DoubleAgent.Controllers.Actors;

namespace DoubleAgent.Controllers.States.Actors
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
        protected override void OnPlayAnimation(AnimationStateMachine_Base stateMachine, string animationKey)
        {
            actor = GetStateMachine<ActorAnimator>().actor;
        }

        //Update
        protected override void OnActiveInStateMachine(AnimationStateMachine_Base stateMachine)
        {
            if (AnimationController != null)
            {
                AnimationController.SetFloat("MovX", actor.ActorData.AnimMovX);
                AnimationController.SetFloat("MovY", actor.ActorData.AnimMovY);
                AnimationController.SetFloat("Speed", actor.ActorData.Speed);
            }
        }

        protected override void OnStateCanceled(AnimationStateMachine_Base stateMachine)
        {
            if (AnimationController != null)
            {
                AnimationController.SetFloat("MovY", 0);
                AnimationController.SetFloat("MovX", 0);
                AnimationController.SetFloat("Speed", 1);
            }
        }
    }
}