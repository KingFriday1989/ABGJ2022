using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DoubleAgent.Data;
using DoubleAgent.Controllers.Actors;

public class ActorHud : MonoBehaviour
{
    public Actor actor;
    public TMP_Text Score;
    public TMP_Text Grenades;
    public TMP_Text GameTime;

    private void Start()
    {
        Grenades.text = actor.ActorData.weapon.grenNum.ToString();
    }

    private void Update()
    {
        Score.text =  GameData.Score.ToString();
    }
}
