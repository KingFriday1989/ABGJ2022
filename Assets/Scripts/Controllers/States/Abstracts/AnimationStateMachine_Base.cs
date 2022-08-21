using Controllers;
using UnityEngine;

namespace DoubleAgent.Controllers.States
{
    public abstract class AnimationStateMachine_Base : AnimationStateMachine<AnimationStateMachine_Base, AnimationState_Base>
    {
        [SerializeField] private Animator _animator;
        public Animator animator => _animator;
    }
}