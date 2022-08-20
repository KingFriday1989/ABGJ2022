using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Controllers.States;

public abstract class AnimationState_Base : Controllers.AnimationState
{
    [SerializeField] string _DefaultAnimationKey;
    public override string DefaultAnimationKey => _DefaultAnimationKey;

    [SerializeField] Animator _AnimationController;
    public override Animator AnimationController => _AnimationController;

    public override bool PlaysAnimationOnActivate => true;

    public virtual bool CanSetState => ActiveStateMachine != null;

    protected T GetStateMachine<T>()
                where T : AnimationStateMachine_Base
    {
        return ActiveStateMachine as T;
    }
}
