using Editor;
using UnityEngine;

namespace DoubleAgent.Data
{
    public class GameDataReader : Core.Behaviour
    {
        [SerializeField, ReadOnly] int Score;

        private void Update()
        {
            Score = GameData.Score;
        }
    }
}