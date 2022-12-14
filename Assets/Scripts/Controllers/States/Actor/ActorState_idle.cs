using DoubleAgent.Controllers.Actors;

namespace DoubleAgent.Controllers.States.Actors
{
    public class ActorState_Idle : AnimationState_Base
    {
        public const string Filename = "Idle";

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
    }
}