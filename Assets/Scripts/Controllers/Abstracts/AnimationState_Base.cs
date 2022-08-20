using Controllers;
using UnityEngine;

namespace DoubleAgent.Controllers.States
{
    public abstract class AnimationState_Base : AnimationState<AnimationStateMachine_Base, AnimationState_Base>
    {
        [SerializeField] string _DefaultAnimationKey;
        public override string DefaultAnimationKey => _DefaultAnimationKey;

        //[SerializeField] Animator _AnimationController;
        public override Animator AnimationController => ActiveStateMachine != null ? ActiveStateMachine.animator : null;

        public override bool PlaysAnimationOnActivate => true;

        public virtual bool CanSetState => ActiveStateMachine != null;

        protected T GetStateMachine<T>() where T : AnimationStateMachine_Base
        => ActiveStateMachine as T;
    }
}