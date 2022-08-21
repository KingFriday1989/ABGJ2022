using DoubleAgent.Data;

namespace DoubleAgent.Controllers.Game
{
    public class ScoreManager : Core.Behaviour
    {
        public void AddScore(int Amount)
        {
            GameData.Score += Amount;
        }
    }
}