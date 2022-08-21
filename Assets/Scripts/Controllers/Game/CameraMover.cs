using DoubleAgent.Controllers.Actors;
using UnityEngine;

namespace DoubleAgent.Controllers.Game
{
    public class CameraMover : Core.Behaviour
    {
        public ActorPlayer ActorPlayer;
        public Vector3 DefaultPos = new Vector3(0, 15, -15);
        
        void Start()
        {
            if (ActorPlayer == null)
                ActorPlayer = FindObjectOfType<ActorPlayer>();
        }

        void Update()
        {
            transform.position = ActorPlayer.transform.position + DefaultPos;
            transform.LookAt(ActorPlayer.transform.position, Vector3.up);
        }
    }
}