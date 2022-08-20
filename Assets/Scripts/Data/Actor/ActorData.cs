using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Data.Actors
{
    public class ActorData : MonoBehaviour
    {
        [Header("General")]
        #region GENERAL
        public bool IsPlayer;
        public List<Renderer> Renderers;
        public Transform ItemHolder;
        #endregion
        [Space(10),Header("Movement")]
        #region MOVEMENT
        public float MovX;
        public float MovY;
        public float Speed;
        public Transform TargetMove;
        #endregion
    }
}

