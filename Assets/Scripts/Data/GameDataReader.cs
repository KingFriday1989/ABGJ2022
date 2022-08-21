using Editor;
using UnityEngine;

namespace DoubleAgent.Data
{
    public class GameDataReader : Core.Behaviour
    {
        [SerializeField, ReadOnly] int Score;
        [SerializeField, ReadOnly] AudioSource GlobalMusicChannel;

        private void Start()
        {
            GlobalMusicChannel = GameData.GlobalMusicChannel;
        }

        private void Update()
        {
            Score = GameData.Score;
        }
    }
}