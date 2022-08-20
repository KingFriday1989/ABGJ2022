using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Controllers.States
{
    public abstract class AnimationStateMachine_Base : Controllers.AnimationStateMachine
    {
        [SerializeField] private Animator _animator;
        public Animator animator => _animator;
    }
}


