using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Data.Actors
{
    public class ActorData : MonoBehaviour
    {
        [Header("General")]
        #region GENERAL
        public bool IsPlayer;
        public NavMeshAgent NavMeshAgent;
        public List<Renderer> Renderers;
        public Transform ItemHolder;
        public Vector3 ClickTarget;
        #endregion
        [Space(10),Header("Movement")]
        #region MOVEMENT
        public float MovX;
        public float MovY;
        public float Speed;
        public bool forward;
        public bool backward;
        public bool stepLeft;
        public bool stepRight;
        #endregion
    }
}