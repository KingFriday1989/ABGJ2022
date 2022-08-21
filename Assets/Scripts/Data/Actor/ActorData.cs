using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using RayFire;

namespace Data.Actors
{
    public class ActorData : MonoBehaviour
    {
        [Header("General")]
        #region GENERAL
        public bool IsPlayer;
        public NavMeshAgent NavMeshAgent;
        public List<Renderer> Renderers;
        public Transform ItemR;
        public Transform ItemL;
        public Vector3 MouseTarget;
        #endregion
        [Space(10),Header("Movement")]
        #region MOVEMENT
        public float MovX;
        public float MovY;
        //public float Mov;
        public float Speed;
        public bool moveForward;
        public bool moveBackward;
        public bool moveStepLeft;
        public bool moveStepRight;
        public bool Sprint;
        public bool isOnGround;
        public Vector3 move;
        public Vector3 velocity;
        #endregion

        #region Animation
        public bool forward;
        public bool backward;
        public bool stepLeft;
        public bool stepRight;
        public float AnimMovX;
        public float AnimMovY;
        #endregion

        #region WEAPONS
        public Weapon weapon;
        public GameObject bombPrefab;
        public RayFire.RayfireGun gun;
        #endregion
    }
}