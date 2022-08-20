using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Controllers.Actors
{
    public class ActorController : MonoBehaviour
    {
        public Actor Actor;

        void Awake()
        {
            if(Actor == null)
                Actor = GetComponent<Actor>();
        }

        void Start()
        {

        }

        void Update()
        {

        }
    }
}

