using Helpers.Extensions;
using RayFire;
using System;
using UnityEngine;
using UnityEngine.Events;

namespace DoubleAgent.Controllers.Utility
{
    //[RequireComponent(typeof(RayfireRigid))]
    public class RayfireEventHandler : DoubleAgentCore
    {
        public const string Filename = "Rayfire Event Handler";

        //[SerializeField] private bool isRoot;
        [Header("Events")]
        [SerializeField] private UnityEvent _OnActivation;
        [SerializeField] private UnityEvent _OnDemolition;
        [SerializeField] private UnityEvent _OnRestriction;

        private RayfireRigid rigid;

        protected override void Awake()
        {
            base.Awake();

            rigid = GetComponent<RayfireRigid>().OrNull(GetComponentInParent<RayfireRigid>());
            if (rigid == null)
                throw new Exception("The Rayfire Event Handler must be a child of a rayfire rigid or attached to a rayfire rigid to subscribe to events.");
            Initialized = true;
        }

        private void OnEnable()
        {
            if (!Initialized) return;
            //if(isGlobalEvent)
            //{
            //    rigid.activationEvent.LocalEvent += Activation;
            //    rigid.demolitionEvent.LocalEvent += Demolition;
            //    rigid.restrictionEvent.LocalEvent += Restriction;
            //}
            //else
            //{
            rigid.activationEvent.LocalEvent += Activation;
            rigid.demolitionEvent.LocalEvent += Demolition;
            rigid.restrictionEvent.LocalEvent += Restriction;
            //}
        }

        private void OnDisable()
        {
            if (!Initialized) return;
            //if(isGlobalEvent)
            //{
            //    rigid.activationEvent.LocalEvent += Activation;
            //    rigid.demolitionEvent.LocalEvent += Demolition;
            //    rigid.restrictionEvent.LocalEvent += Restriction;
            //}
            //else
            //{
            rigid.activationEvent.LocalEvent -= Activation;
            rigid.demolitionEvent.LocalEvent -= Demolition;
            rigid.restrictionEvent.LocalEvent -= Restriction;
            //}
        }

        void Activation(RayfireRigid rigid)
        {
            _OnActivation?.Invoke();
            OnActivation(rigid);
        }
        
        void Demolition(RayfireRigid rigid)
        {
            _OnDemolition?.Invoke();
            OnDemolition(rigid);
        }
        
        void Restriction(RayfireRigid rigid)
        {
            _OnRestriction?.Invoke();
            OnRestriction(rigid);
        }

        protected virtual void OnActivation(RayfireRigid rigid) { }
        protected virtual void OnDemolition(RayfireRigid rigid) { }
        protected virtual void OnRestriction(RayfireRigid rigid) { }
    }
}