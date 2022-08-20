using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Controllers.Actors;

public class CameraMover : MonoBehaviour
{
    public ActorPlayer ActorPlayer;
    public Vector3 DefaultPos = new Vector3(0,15,-15);
    // Start is called before the first frame update
    void Start()
    {
        if(ActorPlayer == null)
        ActorPlayer = FindObjectOfType<ActorPlayer>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = ActorPlayer.transform.position + DefaultPos;
        transform.LookAt(ActorPlayer.transform.position, Vector3.up);
    }
}
