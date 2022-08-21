using DoubleAgent.Data;
using DoubleAgent.Controllers.Actors;
using TMPro;
using UnityEngine;

namespace DoubleAgent.Views.Game
{
    public sealed class ActorHud : Core.Behaviour
    {
        [SerializeField] private Actor actor;
        [SerializeField] private TMP_Text Score;
        [SerializeField] private TMP_Text Grenades;
        [SerializeField] private TMP_Text GameTime;
        [SerializeField] private TMP_Text StartCountdownText;

        private void Start()
        {
            if(Grenades)
                Grenades.text = actor.ActorData.weapon.grenNum.ToString();
        }

        private void Update()
        {
            if(Score)
                Score.text = GameData.Initialized ? GameData.Score.ToString() : "n/a";
            if (Grenades)
                Grenades.text = actor.ActorData.weapon.grenNum.ToString();
        }

        public void StartCountdown(float t)
        {
            int time = (int)t;
            if(StartCountdownText)
                StartCountdownText.text = $"Game Starts In: {time} second{(time > 1 ? "s" : "")}";
        }
    }
}